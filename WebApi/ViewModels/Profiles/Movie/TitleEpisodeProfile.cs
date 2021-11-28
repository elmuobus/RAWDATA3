using AutoMapper;
using WebApi.Domain.MovieDomain;
using WebApi.ViewModels.ListViewModel.Movie;

namespace WebApi.ViewModels.Profiles.Movie
{
    public class TitleEpisodeProfile: Profile
    {
        public TitleEpisodeProfile()
        {
            CreateMap<TitleEpisode, TitleEpisodeListViewModel>();
            CreateMap<TitleEpisodeListViewModel, TitleEpisode>();
        }
    }
}