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
                .ForMember(dest => dest.UserRole, opt =>
                {
                    opt.MapFrom(src => src.UserRoles.Select(ur => ur.Role.Name).First());
                })
                .ForMember(dest => dest.Kids, opt =>
                {
                    opt.MapFrom(src => src.Kids.Select(kid => new KidDetail
                    {
                        Id = kid.KidId,
                        FirstName = kid.Kid.FirstName,
                        LastName = kid.Kid.LastName,
                        Grade = kid.Kid.Grade,
                        Gender = kid.Kid.Gender,
                        Birthday = kid.Kid.Birthday,
                        DateRegistered = kid.Kid.DateRegistered,
                        Parents = kid.Kid.Parents.Select(x => new UserDetail { Id = x.UserId }).ToList(),
                        IsActive = kid.Kid.IsActive,
                    }));
                });

            CreateMap<UserRegisterRequest, User>()
                .ForMember(dest => dest.Organization, opt => opt.Ignore());

            CreateMap<UserUpdateRequest, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserRoles, opt => opt.Ignore())
                .ForMember(dest => dest.Kids, opt =>
                {
                    opt.MapFrom(src => src.Kids.Select(kid => new UserKid { KidId = kid.Id }));
                });

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

            CreateMap<KidInsertRequest, Kid>()
                .ForMember(dest => dest.Parents, opt =>
                  {
                      opt.MapFrom(src => src.Parents.Select(x => new UserKid
                      {
                          UserId = x.Id
                      }));
                  });

            CreateMap<KidUpdateRequest, Kid>();

            CreateMap<KidMemory, CompletedMemory>()
                .ForMember(dest => dest.Points, opt => opt.MapFrom(src => src.Memory.Points))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Memory.Category.Id));

            CreateMap<KidMemory, CompletedMemoryDetail>();
            CreateMap<KidMemoryRequest, KidMemory>();
        }
    }
}
