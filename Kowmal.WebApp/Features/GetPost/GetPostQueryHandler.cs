using AutoMapper;
using Kowmal.WebApp.Services.Interfaces;
using Kowmal.WebApp.ViewModels;
using MediatR;

namespace Kowmal.WebApp.Features.GetPost;

public class GetPostQueryHandler : IRequestHandler<GetPostQuery, PostViewModel>
{
    private readonly IPostService _postService;
    private readonly IMapper _mapper;
    
    public GetPostQueryHandler(IPostService postService, IMapper mapper)
    {
        _postService = postService;
        _mapper = mapper;
    }
    public async Task<PostViewModel> Handle(GetPostQuery request, CancellationToken cancellationToken)
    {
        var posts = await _postService.GetPostAsync(request.Identifier, cancellationToken);

        return _mapper.Map<PostViewModel>(posts);
    }
}