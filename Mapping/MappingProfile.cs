using AutoMapper;
using BlogApi.DTOs;
using BlogApi.Core.Entities;
using BlogApi.Shared.DTOs;

namespace BlogApi.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // User mappings
        CreateMap<User, UserDto>();

        // Post mappings
        CreateMap<Post, PostDto>()
            .ForMember(dest => dest.CommentCount, opt => opt.MapFrom(src => src.Comments.Count))
            .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author));

        // Comment mappings
        CreateMap<Comment, CommentDto>()
            .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author));
    }
}