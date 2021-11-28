using WebApi.Domain.MovieDomain;
using WebApi.Services.MovieServices;
using Xunit;

namespace WebApiTests.MovieTest
{
    public class TitleEpisodeTest
    {
        [Fact]
        public void TitleEpisode_Object_HasDefaultValues()
        {
            var titleEpisode = new TitleEpisode();
            Assert.Null(titleEpisode.Id);
            Assert.Null(titleEpisode.TitleId);
            Assert.Null(titleEpisode.SeasonNumber);
            Assert.Null(titleEpisode.EpisodeNumber);
        }

        [Fact]
        public void GetAllTitleEpisode_ValidFirstPage_ReturnsFirstPage()
        {
            var service = new MovieBusinessLayer();
            var titleEpisodeList = service.GetTitleEpisodes(0, 10);
            Assert.Equal(10, titleEpisodeList.Count);
        }
        
        [Fact]
        public void GetTitleEpisode_ValidId_ReturnsTitleEpisodeObject()
        {
            var service = new MovieBusinessLayer();
            var titleEpisode = service.GetTitleEpisode("tt10168710");
            Assert.Equal("tt10168710", titleEpisode.Id);
            Assert.Equal("tt10050752", titleEpisode.TitleId);
            Assert.Equal(1, titleEpisode.SeasonNumber);
            Assert.Equal(10, titleEpisode.EpisodeNumber);
        }
        
        [Fact]
        public void GetTitleEpisode_InvalidId_ReturnsNullObject()
        {
            var service = new MovieBusinessLayer();
            var titleEpisode = service.GetTitleEpisode("notExist");
            Assert.Null(titleEpisode);
        }
    }
}