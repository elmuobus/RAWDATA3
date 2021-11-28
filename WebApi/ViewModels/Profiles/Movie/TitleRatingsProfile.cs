using AutoMapper;
using WebApi.Domain.MovieDomain;
using WebApi.ViewModels.ListViewModel.Movie;

namespace WebApi.ViewModels.Profiles.Movie
{
    public class TitleRatingsProfile: Profile
    {
        public TitleRatingsProfile()
        {
            CreateMap<TitleRatings, TitleRatingsListViewModel>();
            CreateMap<TitleRatingsListViewModel, TitleRatings>();
        }
    }
}
