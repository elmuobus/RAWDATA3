using AutoMapper;
using WebApi.Domain.MovieDomain;
using WebApi.ViewModels.ListViewModel.Movie;

namespace WebApi.ViewModels.Profiles.Movie
{
    public class TitleCrewProfile: Profile
    {
        public TitleCrewProfile()
        {
            CreateMap<TitleCrew, TitleCrewListViewModel>();
            CreateMap<TitleCrewListViewModel, TitleCrew>();
        }
    }
}