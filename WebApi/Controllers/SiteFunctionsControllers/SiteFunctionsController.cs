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
        public IActionResult Recommended(string titleId, int nbResult) //Works now, maybe only get less results.
        {
            //Should check for null values maybe.
            var recommendedTitles = _siteFunctionsBusinessLayer.Recommended(titleId, nbResult);
            return Ok(recommendedTitles);
        }


        [HttpGet("findcoplayers")]//Maybe just delete as kinda unreliable if there are duplicate names i think?
        public IActionResult FindCoPlayers(string actorName, int nbResult) //Works now, maybe only get less results.
        {
            var CoPlayers = _siteFunctionsBusinessLayer.FindCoPlayers(actorName, nbResult);
            return Ok(CoPlayers);
        }

        [HttpGet("findcoplayersbyid")]
        public IActionResult FindCoPlayersById(string actorId, int nbResult) //Works now, maybe only get less results.
        {
            var CoPlayers = _siteFunctionsBusinessLayer.FindCoPlayersByID(actorId, nbResult);
            return Ok(CoPlayers);
        }

        [HttpGet("popularactorinmovie")]
        public IActionResult PopularActorInMovie(string titleId, int nbResult) //Works now, maybe only get less results.
        {
            var popularActors = _siteFunctionsBusinessLayer.PopularActorsInMovieSearch(titleId, nbResult);
            return Ok(popularActors);
        }

        [HttpGet("popularactorscoplayers")]//probably just popularcoplayers. If that is the functionality. 
        public IActionResult PopularActorsCoPlayers(string actorId, int nbResult) //Works now, maybe only get less results.
        {
            var CoPlayers = _siteFunctionsBusinessLayer.SearchForPopularActorsCoPlayers(actorId, nbResult);
            return Ok(CoPlayers);
        }




    }
}
