using WebApi.Domain.MovieDomain;
using WebApi.Services.MovieServices;
using Xunit;

namespace WebApiTests.MovieTest
{
    public class TitleCrewTest
    {
        [Fact]
        public void TitleCrew_Object_HasDefaultValues()
        {
            var titleCrew = new TitleCrew();
            Assert.Null(titleCrew.Id);
            Assert.Null(titleCrew.Directors);
            Assert.Null(titleCrew.Writers);
        }

        [Fact]
        public void GetAllTitleCrew_ValidFirstPage_ReturnsFirstPage()
        {
            var service = new MovieBusinessLayer();
            var titleCrewList = service.GetTitleCrews(0, 10);
            Assert.Equal(10, titleCrewList.Count);
        }
        
        [Fact]
        public void GetTitleCrew_ValidId_ReturnsTitleCrewObject()
        {
            var service = new MovieBusinessLayer();
            var titleCrew = service.GetTitleCrew("tt10850402");
            Assert.Equal("tt10850402", titleCrew.Id);
            Assert.Equal("nm7686241", titleCrew.Directors);
            Assert.Equal("nm8793246,nm9411045,nm6122134,nm6569580", titleCrew.Writers);
        }
        
        [Fact]
        public void GetTitleCrew_InvalidId_ReturnsNullObject()
        {
            var service = new MovieBusinessLayer();
            var titleCrew = service.GetTitleCrew("notExist");
            Assert.Null(titleCrew);
        }
    }
}