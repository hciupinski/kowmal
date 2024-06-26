using AutoMapper;
using Kowmal.WebApp.Services.Interfaces;
using Kowmal.WebApp.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kowmal.WebApp.Features.GetPosts;

public class GetPostsQueryHandler : IRequestHandler<GetPostsQuery, IEnumerable<PostListItemViewModel>>
{
    private readonly IPostService _postService;
    private readonly IMapper _mapper;
    
    public GetPostsQueryHandler(IPostService postService, IMapper mapper)
    {
        _postService = postService;
        _mapper = mapper;
    }
    public async Task<IEnumerable<PostListItemViewModel>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
    {
        var posts = await _postService
            .GetPostsAsQueryable()
            .OrderBy(x => x.IsPublished)
            .ThenByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
        return _mapper.Map<IEnumerable<PostListItemViewModel>>(posts);
    }
}