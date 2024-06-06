using AutoMapper;
using Kowmal.App.Services.Interfaces;
using Kowmal.App.ViewModels;
using MediatR;

namespace Kowmal.App.Features.GetPosts;

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
        var posts = await _postService.GetPostsAsync(cancellationToken);

        return _mapper.Map<IEnumerable<PostListItemViewModel>>(posts);
    }
}