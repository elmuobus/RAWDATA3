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
    [Route(BaseTitleBasicsRoute)]
    public class TitleBasicsController : APagesController
    {
        private const string BaseTitleBasicsRoute = "api/title/basics";
        private readonly MovieBusinessLayer _movieBusinessLayer;
        private readonly IMapper _mapper;

        public TitleBasicsController(LinkGenerator linkGenerator, IMapper mapper): base(linkGenerator)
        {
            _movieBusinessLayer = new MovieBusinessLayer();
            _mapper = mapper;
        }

        [HttpGet(Name = nameof(GetTitleBasics))]
        public IActionResult GetTitleBasics([FromQuery]PagesQueryString pagesQueryString)
        {
            var titleBasics = _movieBusinessLayer
                .GetTitleBasics(pagesQueryString.Page, pagesQueryString.PageSize)
                .Select(CreateTitleBasicsListViewModel);
            return Ok(CreatePagingResult(
                pagesQueryString.Page,
                pagesQueryString.PageSize,
                _movieBusinessLayer.CountTitleBasics(),
                titleBasics,
                nameof(GetTitleBasics)
            ));
        }

        [HttpGet("{id}", Name = nameof(GetTitleBasic))]
        public IActionResult GetTitleBasic(string id)
        {
            var titleBasic = _movieBusinessLayer.GetTitleBasic(id);

            if (titleBasic == null)
                return NotFound();

            return Ok(titleBasic);
        }
        
        private TitleBasicsListViewModel CreateTitleBasicsListViewModel(TitleBasics titleBasics)
        {
            var model = _mapper.Map<TitleBasicsListViewModel>(titleBasics);
            model.Poster = titleBasics.OmdbData.Poster;
            model.Rating = titleBasics.TitleRatings.AverageRating; 
            model.Url = GetUrlObject(nameof(GetTitleBasic), new {titleBasics.Id});
            return model;
        }
    }
}
