using AutoMapper;
using WebApi.Domain.UserDomain;
using WebApi.ViewModels.ListViewModel.User;

namespace WebApi.ViewModels.Profiles.User
{
    public class SearchHistoryProfile: Profile
    {
        public SearchHistoryProfile()
        {
            CreateMap<SearchHistory, SearchHistoryListViewModel>();
            CreateMap<SearchHistoryListViewModel, SearchHistory>();
        }
    }
}