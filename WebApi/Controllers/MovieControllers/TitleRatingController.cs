using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using WebApi.Domain.MovieDomain;
using WebApi.Services.MovieServices;
using WebApi.ViewModels;
using WebApi.ViewModels.ListViewModel;
using WebApi.ViewModels.ListViewModel.Movie;

namespace WebApi.Controllers.MovieControllers
{
    [ApiController]
    [Route(BaseTitleRatingsRoute)]
    public class TitleRatingController : APagesController
    {
        private const string BaseTitleRatingsRoute = "api/title/ratings";
        private readonly MovieBusinessLayer _movieBusinessLayer;
        private readonly IMapper _mapper;

        public TitleRatingController(LinkGenerator linkGenerator, IMapper mapper): base(linkGenerator)
        {
            _movieBusinessLayer = new MovieBusinessLayer();
            _mapper = mapper;
        }

        [HttpGet(Name = nameof(GetTitleRatings))]
        public IActionResult GetTitleRatings([FromQuery]PagesQueryString pagesQueryString)
        {
            var titleRatings = _movieBusinessLayer
                .GetTitleRatings(pagesQueryString.Page, pagesQueryString.PageSize)
                .Select(CreateTitleRatingsListViewModel);
            return Ok(CreatePagingResult(
                pagesQueryString.Page,
                pagesQueryString.PageSize,
                _movieBusinessLayer.CountTitleRatings(),
                titleRatings,
                nameof(GetTitleRatings)
            ));
        }

        [HttpGet("{id}", Name = nameof(GetTitleRating))]
        public IActionResult GetTitleRating(string id)
        {
            var titleRatings = _movieBusinessLayer.GetTitleRating(id);

            if (titleRatings == null)
                return NotFound();

            return Ok(titleRatings);
        }
        
        private TitleRatingsListViewModel CreateTitleRatingsListViewModel(TitleRatings titleRatings)
        {
            var model = _mapper.Map<TitleRatingsListViewModel>(titleRatings);
            model.Url = GetUrlObject(nameof(GetTitleRating), new {titleRatings.Id});
            return model;
        }
    }
}
