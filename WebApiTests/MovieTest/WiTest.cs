using WebApi.Domain.MovieDomain;
using WebApi.Services.MovieServices;
using Xunit;

namespace WebApiTests.MovieTest
{
    public class WiTest
    {
        [Fact]
        public void Wi_Object_HasDefaultValues()
        {
            var titlePrincipals = new Wi();
            Assert.Null(titlePrincipals.TitleId);
            Assert.Null(titlePrincipals.Word);
            Assert.Null(titlePrincipals.Field);
            Assert.Null(titlePrincipals.Lexeme);
        }

        [Fact]
        public void GetAllWis_ValidFirstPage_ReturnsFirstPage()
        {
            var service = new MovieBusinessLayer();
            var titlePrincipalsList = service.GetWis(0, 10);
            Assert.Equal(10, titlePrincipalsList.Count);
        }
        
        [Fact]
        public void GetWi_ValidId_ReturnsWiObject()
        {
            var service = new MovieBusinessLayer();
            var titlePrincipals = service.GetWi("tt0483131", "village", "p");
            Assert.Equal("tt0483131", titlePrincipals.TitleId);
            Assert.Equal("village", titlePrincipals.Word);
            Assert.Equal("p", titlePrincipals.Field);
            Assert.Equal("villag", titlePrincipals.Lexeme);
        }
        
        [Fact]
        public void GetWi_InvalidTitleId_ReturnsNullObject()
        {
            var service = new MovieBusinessLayer();
            var titlePrincipals = service.GetWi("titleIdNotExist", "village", "p");
            Assert.Null(titlePrincipals);
        }
        
        [Fact]
        public void GetWi_InvalidWord_ReturnsNullObject()
        {
            var service = new MovieBusinessLayer();
            var titlePrincipals = service.GetWi("tt0483131", "notAWord", "p");
            Assert.Null(titlePrincipals);
        }
        
        [Fact]
        public void GetWi_InvalidField_ReturnsNullObject()
        {
            var service = new MovieBusinessLayer();
            var titlePrincipals = service.GetWi("tt0483131", "village", "notAField");
            Assert.Null(titlePrincipals);
        }
    }
}