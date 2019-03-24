using System;
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
                    opt.MapFrom(src => src.UserRoles.Select(ur => ur.Role.Name));
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
                    }));
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

            CreateMap<KidMemory, CompletedMemory>()
                .ForMember(dest => dest.Points, opt => opt.MapFrom(src => src.Memory.Points))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Memory.Category.Id));

            CreateMap<MemoryCategory, KidMemoryCategory>()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Memories, opt =>
                {
                    opt.MapFrom(src => src.Memories.Select(memory => new KidMemoryListItem
                    {
                        MemoryId = memory.Id,
                        MemoryName = memory.Name,
                        MemoryDescription = memory.Description,
                        Points = memory.Points ?? 0,
                    }));
                });

            CreateMap<KidMemory, KidMemoryListItem>();
            CreateMap<KidMemoryParams, KidMemory>();
        }
    }
}
