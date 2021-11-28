using WebApi.Domain.MovieDomain;
using WebApi.Services.MovieServices;
using Xunit;

namespace WebApiTests.MovieTest
{
    public class TitleAkasTest
    {
        [Fact]
        public void TitleAkas_Object_HasDefaultValues()
        {
            var titleAkas = new TitleAkas();
            Assert.Null(titleAkas.TitleId);
            Assert.Equal(0, titleAkas.Ordering);
            Assert.Null(titleAkas.Title);
            Assert.Null(titleAkas.Region);
            Assert.Null(titleAkas.Language);
            Assert.Null(titleAkas.Types);
            Assert.Null(titleAkas.Attributes);
            Assert.False(titleAkas.IsOriginalTitle);
        }

        [Fact]
        public void GetAllTitleAkas_ValidFirstPage_ReturnsFirstPage()
        {
            var service = new MovieBusinessLayer();
            var titleAkasList = service.GetTitleAkas(0, 10);
            Assert.Equal(10, titleAkasList.Count);
        }
        
        [Fact]
        public void GetTitleAkas_ValidId_ReturnsTitleAkasObject()
        {
            var service = new MovieBusinessLayer();
            var titleAkas = service.GetTitleAka("tt0052520", 18);
            Assert.Equal("tt0052520", titleAkas.TitleId);
            Assert.Equal(18, titleAkas.Ordering);
            Assert.Equal("En los límites de la realidad", titleAkas.Title);
            Assert.Equal("ES", titleAkas.Region);
            Assert.Equal("", titleAkas.Language);
            Assert.Equal("alternative", titleAkas.Types);
            Assert.Equal("", titleAkas.Attributes);
            Assert.False(titleAkas.IsOriginalTitle);
        }
        
        [Fact]
        public void GetTitleAkas_InvalidTitleId_ReturnsNullObject()
        {
            var service = new MovieBusinessLayer();
            var titleAkas = service.GetTitleAka("notExist", 18);
            Assert.Null(titleAkas);
        }
        
        [Fact]
        public void GetTitleAkas_InvalidOrdering_ReturnsNullObject()
        {
            var service = new MovieBusinessLayer();
            var titleAkas = service.GetTitleAka("tt0052520", 200);
            Assert.Null(titleAkas);
        }
    }
}