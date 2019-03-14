using System.Linq;
using AutoMapper;
using BibleBlast.API.Dtos;
using BibleBlast.API.Models;

namespace BibleBlast.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserDetail>()
                .ForMember(dest => dest.UserRoles, opt =>
                {
                    opt.MapFrom(src => src.UserRoles.Select(ur => ur.Role.NormalizedName));
                });
            CreateMap<UserRegisterRequest, User>();

            CreateMap<Kid, KidDetail>()
                .ForMember(dest => dest.Parents, opt =>
                {
                    opt.MapFrom(src => src.Parents.Select(p => new UserDetail
                    {
                        Id = p.UserId,
                        FirstName = p.User.FirstName,
                        LastName = p.User.LastName,
                    }));
                });
        }
    }
}
