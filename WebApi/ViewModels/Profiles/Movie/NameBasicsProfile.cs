using AutoMapper;
using WebApi.Domain.MovieDomain;
using WebApi.ViewModels.ListViewModel.Movie;

namespace WebApi.ViewModels.Profiles.Movie
{
    public class NameBasicsProfile : Profile
    {
        public NameBasicsProfile()
        {
            CreateMap<NameBasics, NameBasicsListViewModel>();
            CreateMap<NameBasicsListViewModel, NameBasics>();
        }
    }
}