using Kowmal.WebApp.Models;
using MediatR;

namespace Kowmal.WebApp.Features.CreatePost;

public record CreatePostRequest() : IRequest<Post>
{
    public string Name { get; set; }
    public string Description { get; set; }
}