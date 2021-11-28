using AutoMapper;
using WebApi.Domain.MovieDomain;
using WebApi.ViewModels.ListViewModel.Movie;

namespace WebApi.ViewModels.Profiles.Movie
{
    public class WiProfile: Profile
    {
        public WiProfile()
        {
            CreateMap<Wi, WiListViewModel>();
            CreateMap<WiListViewModel, Wi>();
        }
    }
}
