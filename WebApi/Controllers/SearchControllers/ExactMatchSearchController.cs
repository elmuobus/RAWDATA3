using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services.SearchServices;

namespace WebApi.Controllers.SearchControllers
{
    [ApiController]
    [Route(BaseExactMatchRoute)]
    public class ExactMatchSearchController : Controller
    {
        private const string BaseExactMatchRoute = "api/exactmatch";
        private readonly SearchBusinessLayer _searchBusinessLayer;

        public ExactMatchSearchController()
        {
            _searchBusinessLayer = new SearchBusinessLayer();
        }

        [HttpGet("{strings}")]
        public IActionResult ExactMatchSearch(string strings)
        {

            string[] searchStrings = strings.Split(',');
            var exactMatches = _searchBusinessLayer.ExactMatchSearch(searchStrings);
            return Ok(exactMatches);
        }
    }
}
