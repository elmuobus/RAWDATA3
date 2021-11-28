using System.Collections.Generic;
using System.Linq;
using WebApi.Domain.UserDomain;

namespace WebApi.Services.UserServices
{
    public class UserBusinessLayer
    {
        private readonly PortfolioContext _ctx = new();

        #region User
        public User GetUser(string username)
        {
            return _ctx.Users.FirstOrDefault(x => x.Username == username);
        }

        public User CreateUser(string username, string password, string salt)
        {
            if (GetUser(username) != null)
                return null;
            
            var user = new User()
            {
                Username = username,
                Password = password,
                Salt = salt,
                IsAdmin = false,
                IsAdult = false,
            };
            
            _ctx.Users.Add(user);
            _ctx.SaveChanges();
            return user;
        }

        public bool UpdateUserIsAdmin(string username, bool isAdmin)
        {
            var user = _ctx.Users.FirstOrDefault(x => x.Username == username);

            if (user == null)
                return false;
            user.IsAdmin = isAdmin;
            _ctx.SaveChanges();
            return true;
        }

        public bool DeleteUser(string username)
        {
            var userToRemove = GetUser(username);

            if (userToRemove == null)
                return false;

            DeleteRatings(userToRemove.Username);
            DeleteSearchHistories(userToRemove.Username);
            DeleteNameBookmarks(userToRemove.Username);
            DeleteTitleBookmarks(userToRemove.Username);
            _ctx.Users.Remove(userToRemove);
            return _ctx.SaveChanges() > 0;   
        }
        #endregion
        
        #region Rating
        
        public int CountRatings(string username)
        {
            return _ctx.Ratings.Count(x => x.Username == username);
        }
        
        public IList<Rating> GetRatings(string username, int page, int pageSize)
        {
            return _ctx.Ratings
                .Where(x => x.Username == username)
                .Skip(page * pageSize).Take(pageSize)
                .ToList();
        }

        public Rating GetRating(string username, string titleId)
        {
            return _ctx.Ratings
                .FirstOrDefault(x => x.Username == username && x.TitleId == titleId);
        }
        
        public Rating CreateRating(string username, string titleId, int rate, string comment = null)
        {
            if (GetRating(username, titleId) != null)
                return null;
            
            var rating = new Rating
            {
                Username = username,
                TitleId = titleId,
                Rate = rate,
                Comment = comment,
            };

            _ctx.Ratings.Add(rating);
            _ctx.SaveChanges();
            return rating;
        }
        
        public bool DeleteRating(string username, string titleId)
        {
            var rating = GetRating(username, titleId);

            if (rating == null)
                return false;
            
            _ctx.Ratings.Remove(rating);
            return _ctx.SaveChanges() > 0;
        }
        public bool DeleteRatings(string username)
        {
            var ratings = _ctx.Ratings
                .Where(x => x.Username == username)
                .ToList();

            foreach (var rating in ratings)
            {
                _ctx.Ratings.Remove(rating);
            }
            return _ctx.SaveChanges() > 0;
        }

        public bool UpdateRating(string username, string titleId, int rate, string comment = null)
        {
            var rating = _ctx.Ratings.Find(username, titleId);

            if (rating == null)
                return false;

            rating.Rate = rate;
            rating.Comment = comment;
            
            return _ctx.SaveChanges() > 0;
        }
        #endregion
        
        #region SearchHistory
        
        public int CountSearchHistories(string username)
        {
            return _ctx.SearchHistories.Count(x => x.Username == username);
        }
        
        public IList<SearchHistory> GetSearchHistories(string username, int page, int pageSize)
        {
            return _ctx.SearchHistories
                .Where(x => x.Username == username)
                .Skip(page * pageSize).Take(pageSize)
                .ToList();
        }
        
        public SearchHistory GetSearchHistory(string username, string searchKey)
        {
            return _ctx.SearchHistories
                .FirstOrDefault(x => x.Username == username && x.SearchKey == searchKey);
        }
        
        public SearchHistory CreateSearchHistory(string username, string searchKey)
        {
            if (GetSearchHistory(username, searchKey) != null)
                return null;
            
            var searchHistory = new SearchHistory
            {
                Username = username,
                SearchKey = searchKey,
            };

            _ctx.SearchHistories.Add(searchHistory);
            _ctx.SaveChanges();
            return searchHistory;
        }
        
        public bool DeleteSearchHistory(string username, string searchKey)
        {
            var searchHistoryToRemove = GetSearchHistory(username, searchKey);

            if (searchHistoryToRemove == null)
                return false;
            
            _ctx.SearchHistories.Remove(searchHistoryToRemove);
            return _ctx.SaveChanges() > 0;
        }
        
        public bool DeleteSearchHistories(string username)
        {
            var searchHistories = _ctx.SearchHistories
                .Where(x => x.Username == username)
                .ToList();

            foreach (var searchHistory in searchHistories)
            {
                _ctx.SearchHistories.Remove(searchHistory);
            }
            return _ctx.SaveChanges() > 0;
        }
        
        #endregion
        
        #region NameBookmark

        public int CountNameBookmarks(string username)
        {
            return _ctx.NameBookmarks.Count(x => x.Username == username);
        }

        public IList<NameBookmark> GetNameBookmarks(string username, int page, int pageSize)
        {
            return _ctx.NameBookmarks
                .Where(x => x.Username == username)
                .Skip(page * pageSize).Take(pageSize)
                .ToList();
        }

        public NameBookmark GetNameBookmark(string username, string nameId)
        {
            return _ctx.NameBookmarks
                .FirstOrDefault(x => x.Username == username && x.NameId == nameId);
        }
        
        public NameBookmark CreateNameBookmark(string username, string nameId)
        {
            if (GetNameBookmark(username, nameId) != null)
                return null;
            
            var nameBookmark = new NameBookmark
            {
                Username = username,
                NameId = nameId,
            };

            _ctx.NameBookmarks.Add(nameBookmark);
            _ctx.SaveChanges();
            return nameBookmark;
        }
        
        public bool DeleteNameBookmark(string username, string nameId)
        {
            var nameBookmarkToRemove = GetNameBookmark(username, nameId);

            if (nameBookmarkToRemove == null)
                return false;
            
            _ctx.NameBookmarks.Remove(nameBookmarkToRemove);
            return _ctx.SaveChanges() > 0;
        }
        
        public bool DeleteNameBookmarks(string username)
        {
            var nameBookmarks = _ctx.NameBookmarks
                .Where(x => x.Username == username)
                .ToList();

            foreach (var nameBookmark in nameBookmarks)
            {
                _ctx.NameBookmarks.Remove(nameBookmark);
            }
            return _ctx.SaveChanges() > 0;
        }
        
        #endregion
        
        #region TitleBookmark
        
        public int CountTitleBookmarks(string username)
        {
            return _ctx.TitleBookmarks.Count(x => x.Username == username);
        }
        
        public IList<TitleBookmark> GetTitleBookmarks(string username, int page, int pageSize)
        {
            return _ctx.TitleBookmarks
                .Where(x => x.Username == username)
                .Skip(page * pageSize).Take(pageSize)
                .ToList();
        }

        public TitleBookmark GetTitleBookmark(string username, string titleId)
        {
            return _ctx.TitleBookmarks
                .FirstOrDefault(x => x.Username == username && x.TitleId == titleId);
        }
        
        public TitleBookmark CreateTitleBookmark(string username, string titleId)
        {
            if (GetTitleBookmark(username, titleId) != null)
                return null;
            
            var titleBookmark = new TitleBookmark
            {
                Username = username,
                TitleId = titleId,
            };

            _ctx.TitleBookmarks.Add(titleBookmark);
            _ctx.SaveChanges();
            return titleBookmark;
        }
        
        public bool DeleteTitleBookmark(string username, string titleId)
        {
            var titleBookmarkToRemove = GetTitleBookmark(username, titleId);

            if (titleBookmarkToRemove == null)
                return false;
            
            _ctx.TitleBookmarks.Remove(titleBookmarkToRemove);
            return _ctx.SaveChanges() > 0;
        }
        
        public bool DeleteTitleBookmarks(string username)
        {
            var titleBookmarks = _ctx.TitleBookmarks
                .Where(x => x.Username == username)
                .ToList();

            foreach (var titleBookmark in titleBookmarks)
            {
                _ctx.TitleBookmarks.Remove(titleBookmark);
            }
            return _ctx.SaveChanges() > 0;
        }
        
        #endregion
    }
}