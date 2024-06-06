using AutoMapper;
using Kowmal.App.Models;
using Kowmal.App.ViewModels;

namespace Kowmal.App.MapperProfiles;

public class PostsProfile : Profile
{
    public PostsProfile()
    {
        CreateMap<Post, PostListItemViewModel>();
    }
}