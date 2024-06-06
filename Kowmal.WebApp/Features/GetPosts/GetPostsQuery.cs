using Kowmal.WebApp.ViewModels;
using MediatR;

namespace Kowmal.WebApp.Features.GetPosts;

public record GetPostsQuery : IRequest<IEnumerable<PostListItemViewModel>>
{
    
}