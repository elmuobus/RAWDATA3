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
    [Route(BaseNameBasicsRoute)]
    public class NameBasicsController : APagesController
    {
        private const string BaseNameBasicsRoute = "api/title/name";
        private readonly MovieBusinessLayer _movieBusinessLayer;
        private readonly IMapper _mapper;

        public NameBasicsController(LinkGenerator linkGenerator, IMapper mapper): base(linkGenerator)
        {
            _movieBusinessLayer = new MovieBusinessLayer();
            _mapper = mapper;
        }

        [HttpGet(Name = nameof(GetNameBasics))]
        public IActionResult GetNameBasics([FromQuery]PagesQueryString pagesQueryString)
        {
            var nameBasics = _movieBusinessLayer
                .GetNameBasics(pagesQueryString.Page, pagesQueryString.PageSize)
                .Select(CreateNameBasicsListViewModel);
            return Ok(CreatePagingResult(
                pagesQueryString.Page,
                pagesQueryString.PageSize,
                _movieBusinessLayer.CountNameBasics(),
                nameBasics,
                nameof(GetNameBasics)
            ));
        }

        [HttpGet("{id}", Name = nameof(GetNameBasic))]
        public IActionResult GetNameBasic(string id)
        {
            var nameBasics = _movieBusinessLayer.GetNameBasic(id);

            if (nameBasics == null)
                return NotFound();

            return Ok(nameBasics);
        }
        
        private NameBasicsListViewModel CreateNameBasicsListViewModel(NameBasics nameBasics)
        {
            var model = _mapper.Map<NameBasicsListViewModel>(nameBasics);
            model.Url = GetUrlObject(nameof(GetNameBasic), new {nameBasics.Id});
            return model;
        }
    }
}
