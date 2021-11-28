using WebApi.Domain.UserDomain;
using WebApi.Services.UserServices;
using Xunit;

namespace WebApiTests.UserTest
{
    public class TitleBookmarkServiceTest
    {
        private const string UserName = "TitleBookmarkUser";

        [Fact]
        public void TitleBookmark_Object_HasDefaultValues()
        {
            var rating = new TitleBookmark();
            Assert.Null(rating.Username);
            Assert.Null(rating.TitleId);
        }

        [Fact]
        public void CreateTitleBookmark_ValidData_CreateTitleBookmarkAndReturnsNewObject()
        {
            UserUtils.InitUser(UserName);
            var service = new UserBusinessLayer();
            var rating = service.CreateTitleBookmark(UserName, "tt10111746");
            Assert.Equal(UserName, rating.Username);
            Assert.Equal("tt10111746", rating.TitleId);

            // cleanup
            UserUtils.DeleteUser(UserName);
            service.DeleteTitleBookmark(rating.Username, rating.TitleId);
        }
        
        [Fact]
        public void CreateTitleBookmark_ValidDataButAlreadyExisting_ReturnsNullObject()
        {
            UserUtils.InitUser(UserName);
            var service = new UserBusinessLayer();
            var titleBookmark = service.CreateTitleBookmark(UserName, "tt10111746");
            var sameTitleBookmark = service.CreateTitleBookmark(UserName, "tt10111746");
            Assert.Null(sameTitleBookmark);

            // cleanup
            UserUtils.DeleteUser(UserName);
            service.DeleteTitleBookmark(titleBookmark.Username, titleBookmark.TitleId);
        }
        
        [Fact]
        public void GetAllSearchHistories_ValidUsernameAndBasicPage_ReturnsFirstPage()
        {
            UserUtils.InitUser(UserName);
            var service = new UserBusinessLayer();
            var titleBookmark1 = service.CreateTitleBookmark(UserName, "tt10111746");
            var titleBookmark2 = service.CreateTitleBookmark(UserName, "tt12490740");
            var titleBookmark3 = service.CreateTitleBookmark(UserName, "tt1310664");
            var titleBookmark4 = service.CreateTitleBookmark(UserName, "tt1220221");
            var titleBookmark5 = service.CreateTitleBookmark(UserName, "tt0454854");
            var titleBookmarks = service.GetTitleBookmarks(UserName, 0, 10);
            Assert.Equal(5, titleBookmarks.Count);
            
            // cleanup
            UserUtils.DeleteUser(UserName);
            service.DeleteTitleBookmark(titleBookmark1.Username, titleBookmark1.TitleId);
            service.DeleteTitleBookmark(titleBookmark2.Username, titleBookmark2.TitleId);
            service.DeleteTitleBookmark(titleBookmark3.Username, titleBookmark3.TitleId);
            service.DeleteTitleBookmark(titleBookmark4.Username, titleBookmark4.TitleId);
            service.DeleteTitleBookmark(titleBookmark5.Username, titleBookmark5.TitleId);
        }
        
        [Fact]
        public void GetAllSearchHistories_ValidUsernameAndOutsidePage_ReturnsEmptyList()
        {
            UserUtils.InitUser(UserName);
            var service = new UserBusinessLayer();
            var titleBookmark1 = service.CreateTitleBookmark(UserName, "tt10111746");
            var titleBookmark2 = service.CreateTitleBookmark(UserName, "tt12490740");
            var titleBookmark3 = service.CreateTitleBookmark(UserName, "tt1310664");
            var titleBookmark4 = service.CreateTitleBookmark(UserName, "tt1220221");
            var titleBookmark5 = service.CreateTitleBookmark(UserName, "tt0454854");
            var titleBookmarks = service.GetTitleBookmarks(UserName, 1, 10);
            Assert.Equal(0, titleBookmarks.Count);
            
            // cleanup
            UserUtils.DeleteUser(UserName);
            service.DeleteTitleBookmark(titleBookmark1.Username, titleBookmark1.TitleId);
            service.DeleteTitleBookmark(titleBookmark2.Username, titleBookmark2.TitleId);
            service.DeleteTitleBookmark(titleBookmark3.Username, titleBookmark3.TitleId);
            service.DeleteTitleBookmark(titleBookmark4.Username, titleBookmark4.TitleId);
            service.DeleteTitleBookmark(titleBookmark5.Username, titleBookmark5.TitleId);
        }
        
        [Fact]
        public void GetAllSearchHistories_InValidUsername_ReturnsEmptyList()
        {
            UserUtils.InitUser(UserName);
            var service = new UserBusinessLayer();
            var titleBookmark1 = service.CreateTitleBookmark(UserName, "tt10111746");
            var titleBookmark2 = service.CreateTitleBookmark(UserName, "tt12490740");
            var titleBookmark3 = service.CreateTitleBookmark(UserName, "tt1310664");
            var titleBookmark4 = service.CreateTitleBookmark(UserName, "tt1220221");
            var titleBookmark5 = service.CreateTitleBookmark(UserName, "tt0454854");
            var titleBookmarks = service.GetTitleBookmarks("test2", 0, 10);
            Assert.Equal(0, titleBookmarks.Count);
            
            // cleanup
            UserUtils.DeleteUser(UserName);
            service.DeleteTitleBookmark(titleBookmark1.Username, titleBookmark1.TitleId);
            service.DeleteTitleBookmark(titleBookmark2.Username, titleBookmark2.TitleId);
            service.DeleteTitleBookmark(titleBookmark3.Username, titleBookmark3.TitleId);
            service.DeleteTitleBookmark(titleBookmark4.Username, titleBookmark4.TitleId);
            service.DeleteTitleBookmark(titleBookmark5.Username, titleBookmark5.TitleId);
        }
        
        [Fact]
        public void GetTitleBookmark_ValidUsernameAndTitleId_ReturnsTitleBookmarkObject()
        {
            UserUtils.InitUser(UserName);
            var service = new UserBusinessLayer();
            var createTitleBookmark = service.CreateTitleBookmark(UserName, "tt10111746");
            Assert.NotNull(createTitleBookmark);
            var titleBookmark = service.GetTitleBookmark(UserName, "tt10111746");
            Assert.Equal(UserName, titleBookmark.Username);
            Assert.Equal("tt10111746", titleBookmark.TitleId);
            
            // cleanup
            UserUtils.DeleteUser(UserName);
            service.DeleteTitleBookmark(titleBookmark.Username, titleBookmark.TitleId);
        }
        
        [Fact]
        public void GetTitleBookmark_InvalidUsername_ReturnsNullObject()
        {
            UserUtils.InitUser(UserName);
            var service = new UserBusinessLayer();
            var createTitleBookmark = service.CreateTitleBookmark(UserName, "tt10111746");
            Assert.NotNull(createTitleBookmark);
            var titleBookmark = service.GetTitleBookmark("notExist", "tt10111746");
            Assert.Null(titleBookmark);
            
            // cleanup
            UserUtils.DeleteUser(UserName);
            service.DeleteTitleBookmark(createTitleBookmark.Username, createTitleBookmark.TitleId);
        }
        
        [Fact]
        public void GetTitleBookmark_InvalidTitleId_ReturnsNullObject()
        {
            UserUtils.InitUser(UserName);
            var service = new UserBusinessLayer();
            var createTitleBookmark = service.CreateTitleBookmark(UserName, "tt10111746");
            Assert.NotNull(createTitleBookmark);
            var titleBookmark = service.GetTitleBookmark(UserName, "notExist");
            Assert.Null(titleBookmark);
            
            // cleanup
            UserUtils.DeleteUser(UserName);
            service.DeleteTitleBookmark(createTitleBookmark.Username, createTitleBookmark.TitleId);
        }

        [Fact]
        public void DeleteTitleBookmark_ValidUsernameAndTitleId_RemoveTheTitleBookmark()
        {
            UserUtils.InitUser(UserName);
            var service = new UserBusinessLayer();
            var rating = service.CreateTitleBookmark(UserName, "tt10111746");
            var result = service.DeleteTitleBookmark(rating.Username, rating.TitleId);
            Assert.True(result);
            rating = service.GetTitleBookmark(rating.Username, rating.TitleId);
            Assert.Null(rating);
            
            // cleanup
            UserUtils.DeleteUser(UserName);
        }

        [Fact]
        public void DeleteTitleBookmark_InvalidUsername_ReturnsFalse()
        {
            UserUtils.InitUser(UserName);
            var service = new UserBusinessLayer();
            var titleBookmark = service.CreateTitleBookmark(UserName, "tt10111746");
            var result = service.DeleteTitleBookmark("notExist", "tt10111746");
            Assert.False(result);
            
            // cleanup
            UserUtils.DeleteUser(UserName);
            service.DeleteTitleBookmark(titleBookmark.Username, titleBookmark.TitleId);
        }
        
        [Fact]
        public void DeleteTitleBookmark_InvalidTitleId_ReturnsFalse()
        {
            UserUtils.InitUser(UserName);
            var service = new UserBusinessLayer();
            var titleBookmark = service.CreateTitleBookmark(UserName, "tt10111746");
            var result = service.DeleteTitleBookmark(UserName, "notExist");
            Assert.False(result);
            
            // cleanup
            UserUtils.DeleteUser(UserName);
            service.DeleteTitleBookmark(titleBookmark.Username, titleBookmark.TitleId);
        }
    }
}