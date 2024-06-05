using Kowmal.API.Apis.ViewModels;
using Kowmal.API.Models;
using Kowmal.API.Persistance;
using Kowmal.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kowmal.API.Services;

public class PostService : IPostService
{
    private readonly AppDbContext _dbContext;
    
    public PostService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<PostListItem>> GetPostsAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Posts.Include(x => x.Photos)
            .Select(x => new PostListItem()
        {
            SId = x.SId,
            Identifier = x.Identifier,
            Name = x.Name,
            Description = x.Description,
            PhotosCount = x.Photos.Count,
            IsPublished = x.IsPublished
        }).ToListAsync(cancellationToken);
    }
    
    public async Task<List<Post>> GetPublishedPostsAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Posts.Include(x => x.Photos)
            .Where(x => x.IsPublished)
            .ToListAsync(cancellationToken);
    }
    
    public async Task<Post?> GetPostAsync(Guid identifier, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Posts.FirstOrDefaultAsync(x => x.Identifier == identifier, cancellationToken);
    }
    
    public async Task<Post?> GetPostIncludingPhotosAsync(Guid identifier, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Posts
            .Include(p => p.Photos)
            .FirstOrDefaultAsync(x => x.Identifier == identifier, cancellationToken);
    }
    
    public async Task<Post?> CreatePostAsync(Post post, CancellationToken cancellationToken = default)
    {
        var exists = await _dbContext.Posts.AnyAsync(x => x.Name.Equals(post.Name), cancellationToken);
        
        if(exists)
            throw new Exception("Post already exists");
        
        await _dbContext.Posts.AddAsync(post, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return post;
    }
    
    public async Task<Post?> UpdatePostAsync(Post post, CancellationToken cancellationToken = default)
    {
        _dbContext.Posts.Update(post);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return post;
    }
}