using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services.FunctionalServices;

namespace WebApi.Controllers.SiteFunctionsControllers
{
    [ApiController]
    [Route(BaseSiteFunctionsRoute)]
    public class SiteFunctionsController : Controller
    {
        private const string BaseSiteFunctionsRoute = "api/sitefunctions";
        private readonly SiteFunctionsBusinessLayer _siteFunctionsBusinessLayer;

        public SiteFunctionsController()
        {
            _siteFunctionsBusinessLayer = new SiteFunctionsBusinessLayer();
        }

        [HttpGet("recommended")]
        public IActionResult Recommended(string titleId) //Works now, maybe only get less results.
        {
            //Should check for null values maybe.
            var recommendedTitles = _siteFunctionsBusinessLayer.Recommended(titleId);
            return Ok(recommendedTitles);
        }


        [HttpGet("findcoplayers")]
        public IActionResult FindCoPlayers(string actorName) //Works now, maybe only get less results.
        {
            var CoPlayers = _siteFunctionsBusinessLayer.FindCoPlayers(actorName);
            return Ok(CoPlayers);
        }

        [HttpGet("findcoplayersbyid")]
        public IActionResult FindCoPlayersById(string actorId) //Works now, maybe only get less results.
        {
            var CoPlayers = _siteFunctionsBusinessLayer.FindCoPlayersByID(actorId);
            return Ok(CoPlayers);
        }

        [HttpGet("popularactorinmovie")]
        public IActionResult PopularActorInMovie(string titleId) //Works now, maybe only get less results.
        {
            var popularActors = _siteFunctionsBusinessLayer.PopularActorsInMovieSearch(titleId);
            return Ok(popularActors);
        }

        [HttpGet("popularactorscoplayers")]
        public IActionResult PopularActorsCoPlayers(string actorId) //Works now, maybe only get less results.
        {
            var CoPlayers = _siteFunctionsBusinessLayer.SearchForPopularActorsCoPlayers(actorId);
            return Ok(CoPlayers);
        }




    }
}
