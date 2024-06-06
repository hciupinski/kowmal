using Kowmal.App.Models;
using MediatR;

namespace Kowmal.App.Features.CreatePost;

public record CreatePostRequest() : IRequest<Post>
{
    public string Name { get; set; }
    public string Description { get; set; }
}