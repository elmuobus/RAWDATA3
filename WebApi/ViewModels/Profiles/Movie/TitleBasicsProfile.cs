using AutoMapper;
using WebApi.Domain.MovieDomain;
using WebApi.ViewModels.ListViewModel.Movie;

namespace WebApi.ViewModels.Profiles.Movie
{
    public class TitleBasicsProfile: Profile
    {
        public TitleBasicsProfile()
        {
            CreateMap<TitleBasics, TitleBasicsListViewModel>();
            CreateMap<TitleBasicsListViewModel, TitleBasics>();
            CreateMap<TitleBasics, TitleBasicsViewModel>();
            CreateMap<TitleBasicsViewModel, TitleBasics>();
        }
    }
}
