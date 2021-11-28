using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using WebApi.Attributes;
using WebApi.Domain.MovieDomain;
using WebApi.Domain.UserDomain;
using WebApi.Services.UserServices;
using WebApi.ViewModels;
using WebApi.ViewModels.ListViewModel.Movie;
using WebApi.ViewModels.ListViewModel.User;

namespace WebApi.Controllers.UserControllers
{
    [Authorization]
    [ApiController]
    [Route(BaseUserRoute)]
    public class SearchHistoriesController: APagesController
    {
        private const string BaseUserRoute = "api/users/searchhistories";
        private readonly UserBusinessLayer _userService;
        private readonly IMapper _mapper;

        public SearchHistoriesController(LinkGenerator linkGenerator, IMapper mapper): base(linkGenerator)
        {
            _userService = new UserBusinessLayer();
            _mapper = mapper;
        }

        [HttpGet(Name = nameof(GetSearchHistories))]
        public IActionResult GetSearchHistories([FromQuery]PagesQueryString pagesQueryString)
        {
            try
            {
                if (Request.HttpContext.Items["User"] is not User user)
                    throw new ArgumentException("User not exist");
                var searchHistories = _userService
                    .GetSearchHistories(user.Username, pagesQueryString.Page, pagesQueryString.PageSize)
                    .Select(CreateSearchHistoryListViewModel);
                return Ok(CreatePagingResult(
                    pagesQueryString.Page,
                    pagesQueryString.PageSize,
                    _userService.CountSearchHistories(user.Username),
                    searchHistories,
                    nameof(GetSearchHistories)
                ));
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }
        
        [HttpGet("{searchKey}", Name = nameof(GetSearchHistory))]
        public IActionResult GetSearchHistory(string searchKey)
        {
            try
            {
                if (Request.HttpContext.Items["User"] is not User user)
                    throw new ArgumentException("User not exist");
                return Ok(_userService.GetSearchHistory(user.Username, searchKey));
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }
        
        [HttpPost]
        public IActionResult CreateSearchHistory(CreationSearchHistoryDto dto)
        {
            try
            {
                if (Request.HttpContext.Items["User"] is not User user)
                    throw new ArgumentException("User not exist");
                var rating = _userService.CreateSearchHistory(user.Username, dto.SearchKey);
                
                return Created($"{BaseUserRoute}/{rating.SearchKey}", rating);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }
        
        [HttpDelete("{searchKey}")]
        public IActionResult DeleteSearchHistory(string searchKey)
        {
            try
            {
                if (Request.HttpContext.Items["User"] is not User user)
                    throw new ArgumentException("User not exist");
                var isSucceeded = _userService.DeleteSearchHistory(user.Username, searchKey);

                if (!isSucceeded)
                    return NotFound();
                return Ok();
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }
        
        private SearchHistoryListViewModel CreateSearchHistoryListViewModel(SearchHistory searchHistory)
        {
            var model = _mapper.Map<SearchHistoryListViewModel>(searchHistory);
            model.Url = GetUrlObject(nameof(GetSearchHistory), new {searchHistory.SearchKey});
            return model;
        }
    }
}