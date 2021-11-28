using WebApi.Domain.MovieDomain;
using WebApi.Services.MovieServices;
using Xunit;

namespace WebApiTests.MovieTest
{
    public class OmdbDataTest
    {
        [Fact]
        public void OmdbData_Object_HasDefaultValues()
        {
            var omdbData = new OmdbData();
            Assert.Null(omdbData.Id);
            Assert.Null(omdbData.Poster);
            Assert.Null(omdbData.Awards);
            Assert.Null(omdbData.Plot);
        }

        [Fact]
        public void GetAllOmdbDatas_ValidFirstPage_ReturnsFirstPage()
        {
            var service = new MovieBusinessLayer();
            var omdbDataList = service.GetOmdbDatas(0, 10);
            Assert.Equal(10, omdbDataList.Count);
        }
        
        [Fact]
        public void GetOmdbData_ValidId_ReturnsOmdbDataObject()
        {
            var service = new MovieBusinessLayer();
            var omdbData = service.GetOmdbData("tt5788792");
            Assert.Equal("tt5788792", omdbData.Id);
            Assert.Equal("https://m.media-amazon.com/images/M/" +
                         "MV5BZTFhMDdmODEtN2UwOS00ZjQwLTgxMGYtM2JlMGM3YTUyM2FjXkEyXkFqcGdeQXVyMTYzMDM0NTU@" +
                         "._V1_SX300.jpg", omdbData.Poster);
            Assert.Equal("Won 3 Golden Globes. Another 64 wins & 112 nominations.", omdbData.Awards);
            Assert.Equal("Set in 1950s Manhattan, The Marvelous Mrs. Maisel is a 60-minute dramedy that centers on Miriam " +
                         "\"Midge\" Maisel, a sunny, energetic, sharp, Jewish girl who had her life mapped out for herself: go to college," +
                         " find a husband, have kids, and throw the best Yom Kippur dinners in town. Soon enough, she finds herself exactly " +
                         "where she had hoped to be, living happily with her husband and two children in the Upper West Side. " +
                         "A woman of her time, Midge is a cheerleader wife to a man who dreams of a stand-up comedy career, " +
                         "but her perfect life is turned upside down when her husband suddenly leaves her for another woman. " +
                         "Completely unprepared, Midge is left with no choice but to reevaluate what to do with her life. " +
                         "When she accidentally stumbles onto the stage at a comedy club, she soon discovers her own comedic skills " +
                         "and decides to use this newfound talent to help her rebuild a different life for herself. " +
                         "The series will trace the trajectory of Midge''s journey as she goes on to pursue a career in the male-dominated, " +
                         "stand-up comedy profession, and transforms from uptown housewife to East Village club performer.", omdbData.Plot);
        }
        
        [Fact]
        public void GetOmdbData_InvalidId_ReturnsNullObject()
        {
            var service = new MovieBusinessLayer();
            var omdbData = service.GetOmdbData("notExist");
            Assert.Null(omdbData);
        }
    }
}