using AutoMapper;
using WebApi.Domain.MovieDomain;
using WebApi.ViewModels.ListViewModel.Movie;

namespace WebApi.ViewModels.Profiles.Movie
{
    public class TitleAkasProfile: Profile
    {
        public TitleAkasProfile()
        {
            CreateMap<TitleAkas, TitleAkasListViewModel>();
            CreateMap<TitleAkasListViewModel, TitleAkas>();
        }
    }
}
