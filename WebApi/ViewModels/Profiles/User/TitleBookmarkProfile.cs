using AutoMapper;
using WebApi.Domain.UserDomain;
using WebApi.ViewModels.ListViewModel.User;

namespace WebApi.ViewModels.Profiles.User
{
    public class TitleBookmarkProfile: Profile
    {
        public TitleBookmarkProfile()
        {
            CreateMap<TitleBookmark, TitleBookmarkListViewModel>();
            CreateMap<TitleBookmarkListViewModel, TitleBookmark>();
        }
    }
}