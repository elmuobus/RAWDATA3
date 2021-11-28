using AutoMapper;
using WebApi.Domain.MovieDomain;
using WebApi.ViewModels.ListViewModel.Movie;

namespace WebApi.ViewModels.Profiles.Movie
{
    public class TitlePrincipalsProfile: Profile
    {
        public TitlePrincipalsProfile()
        {
            CreateMap<TitlePrincipals, TitlePrincipalsListViewModel>();
            CreateMap<TitlePrincipalsListViewModel, TitlePrincipals>();
        }
    }
}