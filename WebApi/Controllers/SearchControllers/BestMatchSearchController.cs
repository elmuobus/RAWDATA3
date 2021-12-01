using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services.SearchServices;

namespace WebApi.Controllers.SearchControllers
{
    [ApiController]
    [Route(BaseBestMatchRoute)]
    public class BestMatchSearchController : Controller //APagesController instead? Is it as simple as changing the name?
    {
        private const string BaseBestMatchRoute = "api/bestmatch";
        private readonly SearchBusinessLayer _searchBusinessLayer;

        public BestMatchSearchController()
        {
            _searchBusinessLayer = new SearchBusinessLayer();
        }

        [HttpGet("{strings}")]
        public IActionResult BestMatchSearch(string strings)
        {

            string[] searchStrings = strings.Split(',');
            var bestMatches = _searchBusinessLayer.BestMatchSearch(searchStrings);
            return Ok(bestMatches);
        }
    }
}
