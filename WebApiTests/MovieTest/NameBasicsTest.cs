using WebApi.Domain.MovieDomain;
using WebApi.Services.MovieServices;
using Xunit;

namespace WebApiTests.MovieTest
{
    public class NameBasicsTest
    {
        [Fact]
        public void NameBasics_Object_HasDefaultValues()
        {
            var nameBasics = new NameBasics();
            Assert.Null(nameBasics.Id);
            Assert.Null(nameBasics.PrimaryName);
            Assert.Null(nameBasics.BirthYear);
            Assert.Null(nameBasics.DeathYear);
            Assert.Null(nameBasics.PrimaryProfession);
            Assert.Null(nameBasics.KnownForTitles);
        }

        [Fact]
        public void GetAllNameBasics_ValidFirstPage_ReturnsFirstPage()
        {
            var service = new MovieBusinessLayer();
            var nameBasicsList = service.GetNameBasics(0, 10);
            Assert.Equal(10, nameBasicsList.Count);
        }
        
        [Fact]
        public void GetNameBasics_ValidId_ReturnsNameBasicsObject()
        {
            var service = new MovieBusinessLayer();
            var nameBasics = service.GetNameBasic("nm2188098");
            Assert.Equal("nm2188098", nameBasics.Id);
            Assert.Equal("Nolan Gould", nameBasics.PrimaryName);
            Assert.Equal("1998", nameBasics.BirthYear);
            Assert.Equal("    ", nameBasics.DeathYear);
            Assert.Equal("actor", nameBasics.PrimaryProfession);
            Assert.Equal("tt8124054,tt1442437,tt1758795,tt1632708", nameBasics.KnownForTitles);
        }
        
        [Fact]
        public void GetNameBasics_InvalidId_ReturnsNullObject()
        {
            var service = new MovieBusinessLayer();
            var nameBasics = service.GetNameBasic("notExist");
            Assert.Null(nameBasics);
        }
    }
}