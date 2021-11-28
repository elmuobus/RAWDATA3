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
    [Route(BaseOmdbDataRoute)]
    public class OmdbDataController : APagesController
    {
        private const string BaseOmdbDataRoute = "api/title/omdb";
        private readonly MovieBusinessLayer _movieBusinessLayer;
        private readonly IMapper _mapper;

        public OmdbDataController(LinkGenerator linkGenerator, IMapper mapper): base(linkGenerator)
        {
            _movieBusinessLayer = new MovieBusinessLayer();
            _mapper = mapper;
        }

        [HttpGet(Name = nameof(GetOmdbDatas))]
        public IActionResult GetOmdbDatas([FromQuery]PagesQueryString pagesQueryString)
        {
            var omdbDatas = _movieBusinessLayer
                .GetOmdbDatas(pagesQueryString.Page, pagesQueryString.PageSize)
                .Select(CreateOmdbDataListViewModel);
            return Ok(CreatePagingResult(
                pagesQueryString.Page,
                pagesQueryString.PageSize,
                _movieBusinessLayer.CountOmdbDatas(),
                omdbDatas,
                nameof(GetOmdbDatas)
            ));
        }

        [HttpGet("{id}", Name = nameof(GetOmdbData))]
        public IActionResult GetOmdbData(string id)
        {
            var omdbDatas = _movieBusinessLayer.GetOmdbData(id);

            if (omdbDatas == null)
                return NotFound();

            return Ok(omdbDatas);
        }
        
        private OmdbDataListViewModel CreateOmdbDataListViewModel(OmdbData omdbData)
        {
            var model = _mapper.Map<OmdbDataListViewModel>(omdbData);
            model.Url = GetUrlObject(nameof(GetOmdbData), new {omdbData.Id});
            return model;
        }
    }
}
