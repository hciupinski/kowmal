using Kowmal.API.Models;
using MediatR;

namespace Kowmal.API.Features.CreatePost;

public record CreatePostRequest() : IRequest<Post>
{
    public string Name { get; set; }
    public string Description { get; set; }
}