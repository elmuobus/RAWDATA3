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
    [Route(BaseTitlePrincipalsRoute)]
    public class TitlePrincipalsController : APagesController
    {
        private const string BaseTitlePrincipalsRoute = "api/title/principals";
        private readonly MovieBusinessLayer _movieBusinessLayer;
        private readonly IMapper _mapper;

        public TitlePrincipalsController(LinkGenerator linkGenerator, IMapper mapper): base(linkGenerator)
        {
            _movieBusinessLayer = new MovieBusinessLayer();
            _mapper = mapper;
        }

        [HttpGet(Name = nameof(GetTitlePrincipals))]
        public IActionResult GetTitlePrincipals([FromQuery]PagesQueryString pagesQueryString)
        {
            var titlePrincipals = _movieBusinessLayer
                .GetTitlePrincipals(pagesQueryString.Page, pagesQueryString.PageSize)
                .Select(CreateTitlePrincipalsListViewModel);
            return Ok(CreatePagingResult(
                pagesQueryString.Page,
                pagesQueryString.PageSize,
                _movieBusinessLayer.CountTitlePrincipals(),
                titlePrincipals,
                nameof(GetTitlePrincipals)
            ));
        }


        [HttpGet("{titleId}/{ordering:int}/{nameId}", Name = nameof(GetTitlePrincipal))]
        public IActionResult GetTitlePrincipal(string titleId, int ordering, string nameId)
        {
            var titlePrincipals = _movieBusinessLayer.GetTitlePrincipal(titleId, ordering, nameId);

            if (titlePrincipals == null)
                return NotFound();

            return Ok(titlePrincipals);
        }
        
        private TitlePrincipalsListViewModel CreateTitlePrincipalsListViewModel(TitlePrincipals titlePrincipals)
        {
            var model = _mapper.Map<TitlePrincipalsListViewModel>(titlePrincipals);
            model.Url = GetUrlObject(nameof(GetTitlePrincipal), new
            {
                titlePrincipals.TitleId, titlePrincipals.Ordering, titlePrincipals.NameId
            });
            return model;
        }
    }
}
