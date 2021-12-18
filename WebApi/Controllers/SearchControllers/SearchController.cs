using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using WebApi.Controllers.MovieControllers;
using WebApi.Domain.SearchDomain;
using WebApi.Services.SearchServices;
using WebApi.ViewModels.Search;

namespace WebApi.Controllers.SearchControllers
{
    [ApiController]
    [Route(BaseExactMatchRoute)]
    public class SearchController : Controller
    {
        private const string BaseExactMatchRoute = "api/search";
        private readonly SearchBusinessLayer _searchBusinessLayer;
        private readonly LinkGenerator _linkGenerator;

        public SearchController(LinkGenerator linkGenerator)
        {
            _linkGenerator = linkGenerator;
            _searchBusinessLayer = new SearchBusinessLayer();
        }

        [HttpGet("exactmatch")]//now safe :-)
        public IActionResult ExactMatchSearch(string searchKeys, int nbResult)
        {
            var exactMatches = _searchBusinessLayer.ExactMatchSearch(nbResult, searchKeys);
            return Ok(exactMatches);
        }

        [HttpGet("bestmatch")]
        public IActionResult BestMatchSearch(string searchKeys, int nbResult)//Should probably have nbResult like exactmatch.
        {
            var bestMatches = _searchBusinessLayer.BestMatchSearch(nbResult, searchKeys).Select(CreateBestMatchSearchViewModel);
            return Ok(bestMatches);
        }
        
        [HttpGet("structuredactorsearch")]
        public IActionResult StructuredActorSearch(string title, string plot, string characters, string personNames, int nbResult)
        {
            var actors = _searchBusinessLayer.StructuredActorSearch(title, plot, characters, personNames, nbResult); //Maybe need to change return type in sql function table to varchar, if that issue still occurs on newer DB (that i dont have)
            return Ok(actors);
        }

        [HttpGet("structuredstringsearch")]
        public IActionResult StructuredStringSearch(string title, string plot, string characters, string personNames, int nbResult)//Maybe new name for clarity
        {
            var titles = _searchBusinessLayer.StructuredActorSearch(title, plot, characters, personNames, nbResult); //Maybe need to change return type in sql function table to varchar, if that issue still occurs on newer DB (that i dont have)
            return Ok(titles);
        }

        [HttpGet("simplesearch")] //AKA string_search
        public IActionResult SimpleSearch(string title, string user)//Maybe new name for clarity
        {
            var titles = _searchBusinessLayer.SimpleSearch(title, user); //Maybe need to change return type in sql function table to varchar, if that issue still occurs on newer DB (that i dont have)
            return Ok(titles);
        }

        private BestMatchSearchViewModel CreateBestMatchSearchViewModel(BestMatchSearchResult bestMatch)
        {
            return new BestMatchSearchViewModel() {
                Url =  _linkGenerator.GetPathByAction(HttpContext, "GetTitleBasic", "TitleBasics", new {Id = bestMatch.TitleId}),
                Rank = bestMatch.Rank,
                PrimaryTitle = bestMatch.PrimaryTitle,
            };
        }
    }
}
