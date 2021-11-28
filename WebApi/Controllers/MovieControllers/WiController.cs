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
    [Route(BaseWiRoute)]
    public class WiController : APagesController
    {
        private const string BaseWiRoute = "api/title/wi";
        private readonly MovieBusinessLayer _movieBusinessLayer;
        private readonly IMapper _mapper;

        public WiController(LinkGenerator linkGenerator, IMapper mapper) : base(linkGenerator)
        {
            _movieBusinessLayer = new MovieBusinessLayer();
            _mapper = mapper;
        }

        [HttpGet(Name = nameof(GetWis))]
        public IActionResult GetWis([FromQuery]PagesQueryString pagesQueryString)
        {
            var wis = _movieBusinessLayer
                .GetWis(pagesQueryString.Page, pagesQueryString.PageSize)
                .Select(CreateWiListViewModel);
            return Ok(CreatePagingResult(
                pagesQueryString.Page,
                pagesQueryString.PageSize,
                _movieBusinessLayer.CountWis(),
                wis,
                nameof(GetWis)
            ));
        }

        [HttpGet("{titleId}/{word}/{field}", Name = nameof(GetWi))]
        public IActionResult GetWi(string titleId, string word, string field)
        {
            var wi = _movieBusinessLayer.GetWi(titleId, word, field);

            if (wi == null)
                return NotFound();

            return Ok(wi);
        }
        
        private WiListViewModel CreateWiListViewModel(Wi wi)
        {
            var model = _mapper.Map<WiListViewModel>(wi);
            model.Url = GetUrlObject(nameof(GetWi), new {wi.TitleId, wi.Word, wi.Field});
            return model;
        }
    }
}
