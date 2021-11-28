using WebApi.Domain.MovieDomain;
using WebApi.Services.MovieServices;
using Xunit;

namespace WebApiTests.MovieTest
{
    public class TitleRatingsTest
    {
        [Fact]
        public void TitleRatings_Object_HasDefaultValues()
        {
            var titleRatings = new TitleRatings();
            Assert.Null(titleRatings.Id);
            Assert.Equal(0, titleRatings.AverageRating);
            Assert.Equal(0, titleRatings.NumVotes);
        }

        [Fact]
        public void GetAllTitleRatings_ValidFirstPage_ReturnsFirstPage()
        {
            var service = new MovieBusinessLayer();
            var titleRatingsList = service.GetTitleRatings(0, 10);
            Assert.Equal(10, titleRatingsList.Count);
        }
        
        [Fact]
        public void GetTitleRatings_ValidId_ReturnsTitleRatingsObject()
        {
            var service = new MovieBusinessLayer();
            var titleRatings = service.GetTitleRating("tt0052520");
            Assert.Equal("tt0052520", titleRatings.Id);
            Assert.Equal(9.0, titleRatings.AverageRating);
            Assert.Equal(68643, titleRatings.NumVotes);
        }
        
        [Fact]
        public void GetTitleRatings_InvalidId_ReturnsNullObject()
        {
            var service = new MovieBusinessLayer();
            var titleRatings = service.GetTitleRating("notExist");
            Assert.Null(titleRatings);
        }
    }
}