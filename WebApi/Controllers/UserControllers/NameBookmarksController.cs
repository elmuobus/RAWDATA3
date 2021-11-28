using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using WebApi.Attributes;
using WebApi.Domain.MovieDomain;
using WebApi.Domain.UserDomain;
using WebApi.Services.UserServices;
using WebApi.Utils;
using WebApi.ViewModels;
using WebApi.ViewModels.ListViewModel;
using WebApi.ViewModels.ListViewModel.Movie;
using WebApi.ViewModels.ListViewModel.User;

namespace WebApi.Controllers.UserControllers
{
    [Authorization]
    [ApiController]
    [Route(BaseUserRoute)]
    public class NameBookmarksController: APagesController
    {
        private const string BaseUserRoute = "api/users/namebookmarks";
        private readonly UserBusinessLayer _userService;
        private readonly IMapper _mapper;

        public NameBookmarksController(LinkGenerator linkGenerator, IMapper mapper) : base(linkGenerator)
        {
            _userService = new UserBusinessLayer();
            _mapper = mapper;
        }

        [HttpGet(Name = nameof(GetNameBookmarks))]
        public IActionResult GetNameBookmarks([FromQuery]PagesQueryString pagesQueryString)
        {
            try
            {
                if (Request.HttpContext.Items["User"] is not User user)
                    throw new ArgumentException("User not exist");
                var nameBookmarks = _userService
                    .GetNameBookmarks(user.Username, pagesQueryString.Page, pagesQueryString.PageSize)
                    .Select(CreateNameBookmarkListViewModel);
                return Ok(CreatePagingResult(
                    pagesQueryString.Page,
                    pagesQueryString.PageSize,
                    _userService.CountNameBookmarks(user.Username),
                    nameBookmarks,
                    nameof(GetNameBookmarks)
                ));
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }
        
        [HttpGet("{nameId}", Name = nameof(GetNameBookmark))]
        public IActionResult GetNameBookmark(string nameId)
        {
            try
            {
                if (Request.HttpContext.Items["User"] is not User user)
                    throw new ArgumentException("User not exist");
                return Ok(_userService.GetNameBookmark(user.Username, nameId));
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }
        
        [HttpPost]
        public IActionResult CreateNameBookmark(CreationNameBookmarkDto dto)
        {
            try
            {
                if (Request.HttpContext.Items["User"] is not User user)
                    throw new ArgumentException("User not exist");
                var rating = _userService.CreateNameBookmark(user.Username, dto.NameId);
                
                return Created($"{BaseUserRoute}/{rating.NameId}", rating);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }
        
        [HttpDelete("{nameId}")]
        public IActionResult DeleteNameBookmark(string nameId)
        {
            try
            {
                if (Request.HttpContext.Items["User"] is not User user)
                    throw new ArgumentException("User not exist");
                var isSucceeded = _userService.DeleteNameBookmark(user.Username, nameId);

                if (!isSucceeded)
                    return NotFound();
                return Ok();
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }
        
        private NameBookmarkListViewModel CreateNameBookmarkListViewModel(NameBookmark nameBookmark)
        {
            var model = _mapper.Map<NameBookmarkListViewModel>(nameBookmark);
            model.Url = GetUrlObject(nameof(GetNameBookmark), new {nameBookmark.NameId});
            return model;
        }
    }
}