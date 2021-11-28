using AutoMapper;
using WebApi.Domain.UserDomain;
using WebApi.ViewModels.ListViewModel.User;

namespace WebApi.ViewModels.Profiles.User
{
    public class RatingProfile: Profile
    {
        public RatingProfile()
        {
            CreateMap<Rating, RatingListViewModel>();
            CreateMap<RatingListViewModel, Rating>();
        }
    }
}