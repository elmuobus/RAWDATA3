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
    [Route(BaseTitleCrewRoute)]
    public class TitleCrewController : APagesController
    {
        private const string BaseTitleCrewRoute = "api/title/crews";
        private readonly MovieBusinessLayer _movieBusinessLayer;
        private readonly IMapper _mapper;

        public TitleCrewController(LinkGenerator linkGenerator, IMapper mapper): base(linkGenerator)
        {
            _movieBusinessLayer = new MovieBusinessLayer();
            _mapper = mapper;
        }

        [HttpGet(Name = nameof(GetTitleCrews))]
        public IActionResult GetTitleCrews([FromQuery]PagesQueryString pagesQueryString)
        {
            var titleCrew = _movieBusinessLayer
                .GetTitleCrews(pagesQueryString.Page, pagesQueryString.PageSize)
                .Select(CreateTitleCrewListViewModel);
            return Ok(CreatePagingResult(
                pagesQueryString.Page,
                pagesQueryString.PageSize,
                _movieBusinessLayer.CountTitleCrews(),
                titleCrew,
                nameof(GetTitleCrews)
            ));
        }

        [HttpGet("{id}", Name = nameof(GetTitleCrew))]
        public IActionResult GetTitleCrew(string id)
        {
            var titleCrew = _movieBusinessLayer.GetTitleCrew(id);

            if (titleCrew == null)
                return NotFound();

            return Ok(titleCrew);
        }
        
        private TitleCrewListViewModel CreateTitleCrewListViewModel(TitleCrew titleCrew)
        {
            var model = _mapper.Map<TitleCrewListViewModel>(titleCrew);
            model.Url = GetUrlObject(nameof(GetTitleCrew), new {titleCrew.Id});
            return model;
        }
    }
}
