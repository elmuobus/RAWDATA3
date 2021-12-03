using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using WebApi.Services.SearchServices;

namespace WebApi.Controllers.SearchControllers
{
    [ApiController]
    [Route(BaseExactMatchRoute)]
    public class SearchController : Controller
    {
        private const string BaseExactMatchRoute = "api/search";
        private readonly SearchBusinessLayer _searchBusinessLayer;

        public SearchController()
        {
            _searchBusinessLayer = new SearchBusinessLayer();
        }

        [HttpGet("exactmatch")]
        public IActionResult ExactMatchSearch(string searchKeys, int nbResult)
        {

            string[] searchStrings = searchKeys.Split(',');
            var exactMatches = _searchBusinessLayer.ExactMatchSearch(nbResult, searchStrings);
            return Ok(exactMatches);
        }

        [HttpGet("bestmatch")]
        public IActionResult BestMatchSearch(string strings)
        {

            string[] searchStrings = strings.Split(',');
            var bestMatches = _searchBusinessLayer.BestMatchSearch(searchStrings);
            return Ok(bestMatches);
        }
        /*
        [HttpGet("structuredactorsearch")]
        public IActionResult StructuredActorSearch(string title, string plot, string characters, string personNames)
        {
            var actors = _searchBusinessLayer.BestMatchSearch(searchStrings); //Maybe need to change return type in sql function table to varchar, if that issue still occurs on newer DB (that i dont have)
            return Ok(bestMatches);
        }
        */

    }
}
