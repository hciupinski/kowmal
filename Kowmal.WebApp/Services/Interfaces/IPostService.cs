using Kowmal.WebApp.Models;

namespace Kowmal.WebApp.Services.Interfaces;

public interface IPostService
{
    Task<List<Post>> GetPostsAsync(CancellationToken cancellationToken = default);
    Task<Post?> GetPostAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Post?> CreatePostAsync(Post post, CancellationToken cancellationToken = default);
    Task<Post?> GetPostIncludingPhotosAsync(Guid identifier, CancellationToken cancellationToken = default);
    Task<Post?> UpdatePostAsync(Post post, CancellationToken cancellationToken = default);
    Task<List<Post>> GetPublishedPostsAsync(CancellationToken cancellationToken = default);
    IQueryable<Post> GetPostsAsQueryable();
}