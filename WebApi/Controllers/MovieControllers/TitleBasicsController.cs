using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using WebApi.Domain.MovieDomain;
using WebApi.Services.MovieServices;
using WebApi.ViewModels.ListViewModel.Movie;
using WebApi.ViewModels.Movie;

namespace WebApi.Controllers.MovieControllers
{
    [ApiController]
    [Route(TitleBasicsRoute)]
    public class TitleBasicsController : APagesController
    {
        private const string TitleBasicsRoute = "api/title/basics";
        private readonly MovieBusinessLayer _movieBusinessLayer;
        private readonly IMapper _mapper;

        public TitleBasicsController(LinkGenerator linkGenerator, IMapper mapper): base(linkGenerator)
        {
            _movieBusinessLayer = new MovieBusinessLayer();
            _mapper = mapper;
        }

        public class PagesQueryStringWithSearch : PagesQueryString
        {
            public string SearchTitle { get; set; } = "";
        }

        [HttpGet(Name = nameof(GetTitleBasics))]
        public IActionResult GetTitleBasics([FromQuery]PagesQueryStringWithSearch pagesQueryString)
        {
            var titleBasics = _movieBusinessLayer
                .GetTitleBasics(pagesQueryString.Page, pagesQueryString.PageSize, pagesQueryString.SearchTitle)
                .Select(CreateTitleBasicsListViewModel);
            
            return Ok(CreatePagingResult(
                pagesQueryString.Page,
                pagesQueryString.PageSize,
                _movieBusinessLayer.CountTitleBasics(pagesQueryString.SearchTitle),
                titleBasics,
                nameof(GetTitleBasics)
            ));
        }
        
        public class SpecificPagesQueryStringWithSearch : PagesQueryStringWithSearch
        {
            public string Types { get; set; } = "";
        }
        
        [HttpGet("specific", Name = nameof(GetSpecificBasics))]
        public IActionResult GetSpecificBasics([FromQuery]SpecificPagesQueryStringWithSearch pagesQueryString)
        {
            var types = pagesQueryString.Types.Split(",");
            
            var titleBasics = _movieBusinessLayer
                .GetSpecificBasics(pagesQueryString.Page, pagesQueryString.PageSize, pagesQueryString.SearchTitle, types)
                .Select(CreateTitleBasicsListViewModel);
            return Ok(CreatePagingResult(
                pagesQueryString.Page,
                pagesQueryString.PageSize,
                _movieBusinessLayer.CountSpecificBasics(pagesQueryString.SearchTitle, types),
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

            return Ok(CreateTitleBasicsViewModel(titleBasic));
        }

        private TitleBasicsViewModel CreateTitleBasicsViewModel(TitleBasics titleBasics)
        {
            var model = _mapper.Map<TitleBasicsViewModel>(titleBasics);
            model.Poster = titleBasics.OmdbData?.Poster;
            model.Plot = titleBasics.OmdbData?.Plot;
            model.Rating = titleBasics.TitleRatings?.AverageRating; 
            return model;
        }

        private TitleBasicsListViewModel CreateTitleBasicsListViewModel(TitleBasics titleBasics)
        {
            var model = _mapper.Map<TitleBasicsListViewModel>(titleBasics);
            model.Poster = titleBasics.OmdbData?.Poster;
            model.Rating = titleBasics.TitleRatings?.AverageRating; 
            model.Url = GetUrlObject(nameof(GetTitleBasic), new {titleBasics.Id});
            return model;
        }
    }
}
