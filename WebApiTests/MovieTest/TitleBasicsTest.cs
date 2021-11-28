using WebApi.Domain.MovieDomain;
using WebApi.Services.MovieServices;
using Xunit;

namespace WebApiTests.MovieTest
{
    public class TitleBasicsTest
    {
        [Fact]
        public void TitleBasics_Object_HasDefaultValues()
        {
            var titleBasics = new TitleBasics();
            Assert.Null(titleBasics.Id);
            Assert.Null(titleBasics.TitleType);
            Assert.Null(titleBasics.PrimaryTitle);
            Assert.Null(titleBasics.OriginalTitle);
            Assert.False(titleBasics.IsAdult);
            Assert.Null(titleBasics.StartYear);
            Assert.Null(titleBasics.EndYear);
            Assert.Null(titleBasics.RuntimeMinutes);
            Assert.Null(titleBasics.Genres);
        }

        [Fact]
        public void GetAllTitleBasics_ValidFirstPage_ReturnsFirstPage()
        {
            var service = new MovieBusinessLayer();
            var titleBasicsList = service.GetTitleBasics(0, 10);
            Assert.Equal(10, titleBasicsList.Count);
        }
        
        [Fact]
        public void GetTitleBasics_ValidId_ReturnsTitleBasicsObject()
        {
            var service = new MovieBusinessLayer();
            var titleBasics = service.GetTitleBasic("tt10436314");
            Assert.Equal("tt10436314", titleBasics.Id);
            Assert.Equal("movie", titleBasics.TitleType);
            Assert.Equal("Shut up Sona", titleBasics.PrimaryTitle);
            Assert.Equal("Shut up Sona", titleBasics.OriginalTitle);
            Assert.False(titleBasics.IsAdult);
            Assert.Equal("2020", titleBasics.StartYear);
            Assert.Equal("    ", titleBasics.EndYear);
            Assert.Equal(85, titleBasics.RuntimeMinutes);
            Assert.Equal("Documentary,Musical", titleBasics.Genres);
        }
        
        [Fact]
        public void GetTitleBasics_InvalidId_ReturnsNullObject()
        {
            var service = new MovieBusinessLayer();
            var titleBasics = service.GetTitleBasic("notExist");
            Assert.Null(titleBasics);
        }
    }
}