using WebApi.Domain.MovieDomain;
using WebApi.Services.MovieServices;
using Xunit;

namespace WebApiTests.MovieTest
{
    public class TitlePrincipalsTest
    {
        [Fact]
        public void TitlePrincipals_Object_HasDefaultValues()
        {
            var titlePrincipals = new TitlePrincipals();
            Assert.Null(titlePrincipals.TitleId);
            Assert.Equal(0, titlePrincipals.Ordering);
            Assert.Null(titlePrincipals.NameId);
            Assert.Null(titlePrincipals.Category);
            Assert.Null(titlePrincipals.Job);
            Assert.Null(titlePrincipals.Characters);
        }

        [Fact]
        public void GetAllTitlePrincipals_ValidFirstPage_ReturnsFirstPage()
        {
            var service = new MovieBusinessLayer();
            var titlePrincipalsList = service.GetTitlePrincipals(0, 10);
            Assert.Equal(10, titlePrincipalsList.Count);
        }
        
        [Fact]
        public void GetTitlePrincipals_ValidId_ReturnsTitlePrincipalsObject()
        {
            var service = new MovieBusinessLayer();
            var titlePrincipals = service.GetTitlePrincipal("tt10160588", 3, "nm9605068");
            Assert.Equal("tt10160588", titlePrincipals.TitleId);
            Assert.Equal(3, titlePrincipals.Ordering);
            Assert.Equal("nm9605068", titlePrincipals.NameId);
            Assert.Equal("actor", titlePrincipals.Category);
            Assert.Equal("", titlePrincipals.Job);
            Assert.Equal("['The Narrator']", titlePrincipals.Characters);
        }
        
        [Fact]
        public void GetTitlePrincipals_InvalidTitleId_ReturnsNullObject()
        {
            var service = new MovieBusinessLayer();
            var titlePrincipals = service.GetTitlePrincipal("titleIdNotExist", 3, "nm9605068");
            Assert.Null(titlePrincipals);
        }
        
        [Fact]
        public void GetTitlePrincipals_InvalidOrdering_ReturnsNullObject()
        {
            var service = new MovieBusinessLayer();
            var titlePrincipals = service.GetTitlePrincipal("tt10160588", 200, "nm9605068");
            Assert.Null(titlePrincipals);
        }
        
        [Fact]
        public void GetTitlePrincipals_InvalidNameId_ReturnsNullObject()
        {
            var service = new MovieBusinessLayer();
            var titlePrincipals = service.GetTitlePrincipal("tt10160588", 3, "nameIdNotExist");
            Assert.Null(titlePrincipals);
        }
    }
}