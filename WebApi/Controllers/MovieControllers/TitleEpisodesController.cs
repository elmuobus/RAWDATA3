using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using WebApi.Attributes;
using WebApi.Domain.MovieDomain;
using WebApi.Services.MovieServices;
using WebApi.ViewModels;
using WebApi.ViewModels.ListViewModel;
using WebApi.ViewModels.ListViewModel.Movie;

namespace WebApi.Controllers.MovieControllers
{
    [ApiController]
    [Route(BaseTitleEpisodesRoute)]
    public class TitleEpisodesController: APagesController
    {
        private const string BaseTitleEpisodesRoute = "api/title/episodes";
        private readonly MovieBusinessLayer _movieBusinessLayer;
        private readonly IMapper _mapper;

        public TitleEpisodesController(LinkGenerator linkGenerator, IMapper mapper): base(linkGenerator)
        {
            _movieBusinessLayer = new MovieBusinessLayer();
            _mapper = mapper;
        }
        
        [HttpGet(Name = nameof(GetTitleEpisodes))]
        public IActionResult GetTitleEpisodes([FromQuery]PagesQueryString pagesQueryString)
        {
            var titleEpisodes = _movieBusinessLayer
                .GetTitleEpisodes(pagesQueryString.Page, pagesQueryString.PageSize)
                .Select(CreateTitleEpisodeListViewModel);
            return Ok(CreatePagingResult(
                pagesQueryString.Page,
                pagesQueryString.PageSize,
                _movieBusinessLayer.CountTitleEpisodes(),
                titleEpisodes,
                nameof(GetTitleEpisodes)
            ));
        }

        [HttpGet("{id}", Name = nameof(GetTitleEpisode))]
        public IActionResult GetTitleEpisode(string id)
        {
            var titleEpisode = _movieBusinessLayer.GetTitleEpisode(id);

            if (titleEpisode == null)
                return NotFound();

            return Ok(titleEpisode);
        }
        
        private TitleEpisodeListViewModel CreateTitleEpisodeListViewModel(TitleEpisode titleEpisode)
        {
            var model = _mapper.Map<TitleEpisodeListViewModel>(titleEpisode);
            model.Url = GetUrlObject(nameof(GetTitleEpisode), new {titleEpisode.Id});
            return model;
        }
    }
}