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
    [Route(BaseTitleAkasRoute)]
    public class TitleAkasController : APagesController
    {
        private const string BaseTitleAkasRoute = "api/title/akas";
        private readonly MovieBusinessLayer _movieBusinessLayer;
        private readonly IMapper _mapper;

        public TitleAkasController(LinkGenerator linkGenerator, IMapper mapper): base(linkGenerator)
        {
            _movieBusinessLayer = new MovieBusinessLayer();
            _mapper = mapper;
        }

        [HttpGet(Name = nameof(GetTitleAkas))]
        public IActionResult GetTitleAkas([FromQuery]PagesQueryString pagesQueryString)
        {
            var titleAkas = _movieBusinessLayer
                .GetTitleAkas(pagesQueryString.Page, pagesQueryString.PageSize)
                .Select(CreateTitleAkasListViewModel);
            return Ok(CreatePagingResult(
                pagesQueryString.Page,
                pagesQueryString.PageSize,
                _movieBusinessLayer.CountTitleAkas(),
                titleAkas,
                nameof(GetTitleAkas)
            ));
        }

        [HttpGet("{id}/{ordering:int}", Name = nameof(GetTitleAka))]
        public IActionResult GetTitleAka(string id, int ordering)
        {
            var titleAkas = _movieBusinessLayer.GetTitleAka(id, ordering);

            if (titleAkas == null)
                return NotFound();

            return Ok(titleAkas);
        }
        
        private TitleAkasListViewModel CreateTitleAkasListViewModel(TitleAkas titleAkas)
        {
            var model = _mapper.Map<TitleAkasListViewModel>(titleAkas);
            model.Url = GetUrlObject(nameof(GetTitleAka), new {titleAkas.TitleId, titleAkas.Ordering});
            return model;
        }
    }
}
