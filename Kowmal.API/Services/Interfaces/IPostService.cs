using Kowmal.API.Apis.ViewModels;
using Kowmal.API.Models;

namespace Kowmal.API.Services.Interfaces;

public interface IPostService
{
    Task<List<PostListItem>> GetPostsAsync(CancellationToken cancellationToken = default);
    Task<Post?> GetPostAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Post?> CreatePostAsync(Post post, CancellationToken cancellationToken = default);
    Task<Post?> GetPostIncludingPhotosAsync(Guid identifier, CancellationToken cancellationToken = default);
    Task<Post?> UpdatePostAsync(Post post, CancellationToken cancellationToken = default);
    Task<List<Post>> GetPublishedPostsAsync(CancellationToken cancellationToken = default);
}