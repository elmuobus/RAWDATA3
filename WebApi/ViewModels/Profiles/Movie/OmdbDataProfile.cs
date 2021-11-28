using AutoMapper;
using WebApi.Domain.MovieDomain;
using WebApi.ViewModels.ListViewModel.Movie;

namespace WebApi.ViewModels.Profiles.Movie
{
    public class OmdbDataProfile : Profile
    {
        public OmdbDataProfile()
        {
            CreateMap<OmdbData, OmdbDataListViewModel>();
            CreateMap<OmdbDataListViewModel, OmdbData>();
        }
    }
}