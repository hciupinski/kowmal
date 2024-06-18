using AutoMapper;
using Kowmal.WebApp.Models;
using Kowmal.WebApp.ViewModels;

namespace Kowmal.WebApp.MapperProfiles;

public class PostsProfile : Profile
{
    public PostsProfile()
    {
        CreateMap<Post, PostListItemViewModel>();
        CreateMap<Post, PostViewModel>()
            .ForMember(d => d.UploadedFileNames, o => o.MapFrom(s => s.Photos.Select(p => p.FileName).ToList()));
    }
}