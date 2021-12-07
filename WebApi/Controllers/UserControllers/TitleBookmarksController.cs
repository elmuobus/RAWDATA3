using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using WebApi.Attributes;
using WebApi.Domain.UserDomain;
using WebApi.Services.UserServices;
using WebApi.ViewModels;
using WebApi.ViewModels.ListViewModel.User;

namespace WebApi.Controllers.UserControllers
{
    [Authorization]
    [ApiController]
    [Route(UserRoute)]
    public class TitleBookmarksController: APagesController
    {
        private const string UserRoute = "api/users/titlebookmarks";
        private readonly UserBusinessLayer _userService;
        private readonly IMapper _mapper;

        public TitleBookmarksController(LinkGenerator linkGenerator, IMapper mapper) : base(linkGenerator)
        {
            _userService = new UserBusinessLayer();
            _mapper = mapper;
        }

        [HttpGet(Name = nameof(GetTitleBookmarks))]
        public IActionResult GetTitleBookmarks([FromQuery]PagesQueryString pagesQueryString)
        {
            try
            {
                if (Request.HttpContext.Items["User"] is not User user)
                    throw new ArgumentException("User not exist");

                Console.WriteLine(_userService.GetTitleBookmarks(user.Username, pagesQueryString.Page, pagesQueryString.PageSize).Count);
                
                var titleBookmarks = _userService
                    .GetTitleBookmarks(user.Username, pagesQueryString.Page, pagesQueryString.PageSize)
                    .Select(CreateTitleBookmarkListViewModel);
                return Ok(CreatePagingResult(
                    pagesQueryString.Page,
                    pagesQueryString.PageSize,
                    _userService.CountTitleBookmarks(user.Username),
                    titleBookmarks,
                    nameof(GetTitleBookmarks)
                ));
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }
        
        [HttpGet("{titleId}", Name = nameof(GetTitleBookmark))]
        public IActionResult GetTitleBookmark(string titleId)
        {
            try
            {
                if (Request.HttpContext.Items["User"] is not User user)
                    throw new ArgumentException("User not exist");
                return Ok(_userService.GetTitleBookmark(user.Username, titleId));
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }
        
        [HttpPost]
        public IActionResult CreateTitleBookmark(CreationTitleBookmarkDto dto)
        {
            try
            {
                if (Request.HttpContext.Items["User"] is not User user)
                    throw new ArgumentException("User not exist");
                var rating = _userService.CreateTitleBookmark(user.Username, dto.TitleId);
                
                return Created($"{UserRoute}/{rating.TitleId}", rating);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }
        
        [HttpDelete("{titleId}")]
        public IActionResult DeleteTitleBookmark(string titleId)
        {
            try
            {
                if (Request.HttpContext.Items["User"] is not User user)
                    throw new ArgumentException("User not exist");
                var isSucceeded = _userService.DeleteTitleBookmark(user.Username, titleId);

                if (!isSucceeded)
                    return NotFound();
                return Ok();
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }
        
        private TitleBookmarkListViewModel CreateTitleBookmarkListViewModel(TitleBookmark titleBookmark)
        {
            var model = _mapper.Map<TitleBookmarkListViewModel>(titleBookmark);
            model.OriginalTitle = titleBookmark.TitleBasics.OriginalTitle;
            model.Genres = titleBookmark.TitleBasics.Genres;
            model.Poster = titleBookmark.TitleBasics.OmdbData?.Poster;
            model.Rating = titleBookmark.TitleBasics.TitleRatings?.AverageRating;
            model.Url = GetUrlObject(nameof(GetTitleBookmark), new {titleBookmark.TitleId});
            return model;
        }
    }
}