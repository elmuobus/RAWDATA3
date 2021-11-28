using WebApi.Domain.UserDomain;
using WebApi.Services.UserServices;
using Xunit;

namespace WebApiTests.UserTest
{
    public class SearchHistoryServiceTest
    {
        private const string UserName = "SearchHistoryUser";

        [Fact]
        public void SearchHistory_Object_HasDefaultValues()
        {
            var rating = new SearchHistory();
            Assert.Null(rating.Username);
            Assert.Null(rating.SearchKey);
        }

        [Fact]
        public void CreateSearchHistory_ValidData_CreteSearchHistoryAndReturnsNewObject()
        {
            UserUtils.InitUser(UserName);
            var service = new UserBusinessLayer();
            var rating = service.CreateSearchHistory(UserName, "money");
            Assert.Equal(UserName, rating.Username);
            Assert.Equal("money", rating.SearchKey);

            // cleanup
            UserUtils.DeleteUser(UserName);
            service.DeleteSearchHistory(rating.Username, rating.SearchKey);
        }
        
        [Fact]
        public void CreateSearchHistory_ValidDataButAlreadyExisting_ReturnsNullObject()
        {
            UserUtils.InitUser(UserName);
            var service = new UserBusinessLayer();
            var searchHistory = service.CreateSearchHistory(UserName, "money");
            var sameSearchHistory = service.CreateSearchHistory(UserName, "money");
            Assert.Null(sameSearchHistory);

            // cleanup
            UserUtils.DeleteUser(UserName);
            service.DeleteSearchHistory(searchHistory.Username, searchHistory.SearchKey);
        }
        
        [Fact]
        public void GetAllSearchHistories_ValidUsernameAndBasicPage_ReturnsFirstPage()
        {
            UserUtils.InitUser(UserName);
            var service = new UserBusinessLayer();
            var searchHistory1 = service.CreateSearchHistory(UserName, "money");
            var searchHistory2 = service.CreateSearchHistory(UserName, "honey");
            var searchHistory3 = service.CreateSearchHistory(UserName, "bendy");
            var searchHistory4 = service.CreateSearchHistory(UserName, "johnny");
            var searchHistory5 = service.CreateSearchHistory(UserName, "freddy");
            var searchHistories = service.GetSearchHistories(UserName, 0, 10);
            Assert.Equal(5, searchHistories.Count);
            
            // cleanup
            UserUtils.DeleteUser(UserName);
            service.DeleteSearchHistory(searchHistory1.Username, searchHistory1.SearchKey);
            service.DeleteSearchHistory(searchHistory2.Username, searchHistory2.SearchKey);
            service.DeleteSearchHistory(searchHistory3.Username, searchHistory3.SearchKey);
            service.DeleteSearchHistory(searchHistory4.Username, searchHistory4.SearchKey);
            service.DeleteSearchHistory(searchHistory5.Username, searchHistory5.SearchKey);
        }
        
        [Fact]
        public void GetAllSearchHistories_ValidUsernameAndOutsidePage_ReturnsEmptyList()
        {
            UserUtils.InitUser(UserName);
            var service = new UserBusinessLayer();
            var searchHistory1 = service.CreateSearchHistory(UserName, "money");
            var searchHistory2 = service.CreateSearchHistory(UserName, "honey");
            var searchHistory3 = service.CreateSearchHistory(UserName, "bendy");
            var searchHistory4 = service.CreateSearchHistory(UserName, "johnny");
            var searchHistory5 = service.CreateSearchHistory(UserName, "freddy");
            var searchHistories = service.GetSearchHistories(UserName, 1, 10);
            Assert.Equal(0, searchHistories.Count);
            
            // cleanup
            UserUtils.DeleteUser(UserName);
            service.DeleteSearchHistory(searchHistory1.Username, searchHistory1.SearchKey);
            service.DeleteSearchHistory(searchHistory2.Username, searchHistory2.SearchKey);
            service.DeleteSearchHistory(searchHistory3.Username, searchHistory3.SearchKey);
            service.DeleteSearchHistory(searchHistory4.Username, searchHistory4.SearchKey);
            service.DeleteSearchHistory(searchHistory5.Username, searchHistory5.SearchKey);
        }
        
        [Fact]
        public void GetAllSearchHistories_InValidUsername_ReturnsEmptyList()
        {
            UserUtils.InitUser(UserName);
            var service = new UserBusinessLayer();
            var searchHistory1 = service.CreateSearchHistory(UserName, "money");
            var searchHistory2 = service.CreateSearchHistory(UserName, "honey");
            var searchHistory3 = service.CreateSearchHistory(UserName, "bendy");
            var searchHistory4 = service.CreateSearchHistory(UserName, "johnny");
            var searchHistory5 = service.CreateSearchHistory(UserName, "freddy");
            var searchHistories = service.GetSearchHistories("test2", 0, 10);
            Assert.Equal(0, searchHistories.Count);
            
            // cleanup
            UserUtils.DeleteUser(UserName);
            service.DeleteSearchHistory(searchHistory1.Username, searchHistory1.SearchKey);
            service.DeleteSearchHistory(searchHistory2.Username, searchHistory2.SearchKey);
            service.DeleteSearchHistory(searchHistory3.Username, searchHistory3.SearchKey);
            service.DeleteSearchHistory(searchHistory4.Username, searchHistory4.SearchKey);
            service.DeleteSearchHistory(searchHistory5.Username, searchHistory5.SearchKey);
        }
        
        [Fact]
        public void GetSearchHistory_ValidUsernameAndSearchKey_ReturnsSearchHistoryObject()
        {
            UserUtils.InitUser(UserName);
            var service = new UserBusinessLayer();
            var createSearchHistory = service.CreateSearchHistory(UserName, "money");
            Assert.NotNull(createSearchHistory);
            var searchHistory = service.GetSearchHistory(UserName, "money");
            Assert.Equal(UserName, searchHistory.Username);
            Assert.Equal("money", searchHistory.SearchKey);
            
            // cleanup
            UserUtils.DeleteUser(UserName);
            service.DeleteSearchHistory(searchHistory.Username, searchHistory.SearchKey);
        }
        
        [Fact]
        public void GetSearchHistory_InvalidUsername_ReturnsNullObject()
        {
            UserUtils.InitUser(UserName);
            var service = new UserBusinessLayer();
            var createSearchHistory = service.CreateSearchHistory(UserName, "money");
            Assert.NotNull(createSearchHistory);
            var searchHistory = service.GetSearchHistory("notExist", "money");
            Assert.Null(searchHistory);
            
            // cleanup
            UserUtils.DeleteUser(UserName);
            service.DeleteSearchHistory(createSearchHistory.Username, createSearchHistory.SearchKey);
        }
        
        [Fact]
        public void GetSearchHistory_InvalidSearchKey_ReturnsNullObject()
        {
            UserUtils.InitUser(UserName);
            var service = new UserBusinessLayer();
            var createSearchHistory = service.CreateSearchHistory(UserName, "money");
            Assert.NotNull(createSearchHistory);
            var searchHistory = service.GetSearchHistory(UserName, "notExist");
            Assert.Null(searchHistory);
            
            // cleanup
            UserUtils.DeleteUser(UserName);
            service.DeleteSearchHistory(createSearchHistory.Username, createSearchHistory.SearchKey);
        }

        [Fact]
        public void DeleteSearchHistory_ValidUsernameAndSearchKey_RemoveTheSearchHistory()
        {
            UserUtils.InitUser(UserName);
            var service = new UserBusinessLayer();
            var searchHistory = service.CreateSearchHistory(UserName, "money");
            var result = service.DeleteSearchHistory(searchHistory.Username, searchHistory.SearchKey);
            Assert.True(result);
            searchHistory = service.GetSearchHistory(searchHistory.Username, searchHistory.SearchKey);
            Assert.Null(searchHistory);
            
            // cleanup
            UserUtils.DeleteUser(UserName);
        }

        [Fact]
        public void DeleteSearchHistory_InvalidUsername_ReturnsFalse()
        {
            UserUtils.InitUser(UserName);
            var service = new UserBusinessLayer();
            var searchHistory = service.CreateSearchHistory(UserName, "money");
            var result = service.DeleteSearchHistory("notExist", "money");
            Assert.False(result);
            
            // cleanup
            UserUtils.DeleteUser(UserName);
            service.DeleteSearchHistory(searchHistory.Username, searchHistory.SearchKey);
        }
        
        [Fact]
        public void DeleteSearchHistory_InvalidSearchKey_ReturnsFalse()
        {
            UserUtils.InitUser(UserName);
            var service = new UserBusinessLayer();
            var searchHistory = service.CreateSearchHistory(UserName, "money");
            var result = service.DeleteSearchHistory(UserName, "notExist");
            Assert.False(result);
            
            // cleanup
            UserUtils.DeleteUser(UserName);
            service.DeleteSearchHistory(searchHistory.Username, searchHistory.SearchKey);
        }
    }
}