using Kowmal.WebApp.Models;
using Kowmal.WebApp.Services.Interfaces;
using MediatR;

namespace Kowmal.WebApp.Features.CreatePost;

public class CreatePostHandler : IRequestHandler<CreatePostRequest, Post>
{
    private readonly IPostService _postService;
    
    public CreatePostHandler(IPostService postService)
    {
        _postService = postService;
    }
    public async Task<Post> Handle(CreatePostRequest request, CancellationToken cancellationToken)
    {
        var post = new Post().SetDetails(request.Name, request.Description);
        
        var result = await _postService.CreatePostAsync(post, cancellationToken);
        
        if (result == null)
            throw new Exception("Failed to create post");

        return result;
    }
}