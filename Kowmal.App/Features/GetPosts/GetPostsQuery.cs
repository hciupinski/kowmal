using Kowmal.App.ViewModels;
using MediatR;

namespace Kowmal.App.Features.GetPosts;

public record GetPostsQuery : IRequest<IEnumerable<PostListItemViewModel>>
{
    
}