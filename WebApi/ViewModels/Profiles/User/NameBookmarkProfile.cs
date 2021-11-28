using AutoMapper;
using WebApi.Domain.UserDomain;
using WebApi.ViewModels.ListViewModel.User;

namespace WebApi.ViewModels.Profiles.User
{
    public class NameBookmarkProfile: Profile
    {
        public NameBookmarkProfile()
        {
            CreateMap<NameBookmark, NameBookmarkListViewModel>();
            CreateMap<NameBookmarkListViewModel, NameBookmark>();
        }
    }
}