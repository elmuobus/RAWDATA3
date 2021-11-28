using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using WebApi.Attributes;
using WebApi.Domain.MovieDomain;
using WebApi.Domain.UserDomain;
using WebApi.Services.UserServices;
using WebApi.Utils;
using WebApi.ViewModels;
using WebApi.ViewModels.ListViewModel.Movie;
using WebApi.ViewModels.ListViewModel.User;

namespace WebApi.Controllers.UserControllers
{
    [Authorization]
    [ApiController]
    [Route(BaseUserRoute)]
    public class RatingsController: APagesController
    {
        private const string BaseUserRoute = "api/users/ratings";
        private readonly UserBusinessLayer _userService;
        private readonly IMapper _mapper;

        public RatingsController(LinkGenerator linkGenerator, IMapper mapper) : base(linkGenerator)
        {
            _userService = new UserBusinessLayer();
            _mapper = mapper;
        }

        [HttpGet(Name = nameof(GetRatings))]
        public IActionResult GetRatings([FromQuery]PagesQueryString pagesQueryString)
        {
            try
            {
                if (Request.HttpContext.Items["User"] is not User user)
                    throw new ArgumentException("User not exist");
                var ratings = _userService
                    .GetRatings(user.Username, pagesQueryString.Page, pagesQueryString.PageSize)
                    .Select(CreateRatingListViewModel);
                return Ok(CreatePagingResult(
                    pagesQueryString.Page,
                    pagesQueryString.PageSize,
                    _userService.CountRatings(user.Username),
                    ratings,
                    nameof(GetRatings)
                ));
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }
        
        [HttpGet("{titleId}", Name = nameof(GetRating))]
        public IActionResult GetRating(string titleId)
        {
            try
            {
                if (Request.HttpContext.Items["User"] is not User user)
                    throw new ArgumentException("User not exist");
                return Ok(_userService.GetRating(user.Username, titleId));
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }
        
        [HttpPost]
        public IActionResult CreateRating(CreationRatingDto dto)
        {
            try
            {
                if (Request.HttpContext.Items["User"] is not User user)
                    throw new ArgumentException("User not exist");
                var rating = _userService.CreateRating(user.Username, dto.TitleId, dto.Rate, dto.Comment);
                
                return Created($"{BaseUserRoute}/{rating.TitleId}", rating);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }
        
        [HttpPut("{titleId}")]
        public IActionResult UpdateRating(string titleId, UpdateRatingDto dto)
        {
            try
            {
                if (Request.HttpContext.Items["User"] is not User user)
                    throw new ArgumentException("User not exist");
                var isSucceeded = _userService.UpdateRating(user.Username, titleId, dto.Rate, dto.Comment);

                if (!isSucceeded)
                    return NotFound();
                return Ok();
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }
        
        [HttpDelete("{titleId}")]
        public IActionResult DeleteRating(string titleId)
        {
            try
            {
                if (Request.HttpContext.Items["User"] is not User user)
                    throw new ArgumentException("User not exist");
                var isSucceeded = _userService.DeleteRating(user.Username, titleId);

                if (!isSucceeded)
                    return NotFound();
                return Ok();
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }
        
        private RatingListViewModel CreateRatingListViewModel(Rating rating)
        {
            var model = _mapper.Map<RatingListViewModel>(rating);
            model.Url = GetUrlObject(nameof(GetRating), new {rating.TitleId});
            return model;
        }
    }
}