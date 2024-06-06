using System.Text;
using System.Text.Json;
using Kowmal.WebApp.Clients.Interfaces;
using Kowmal.WebApp.Extensions;
using Kowmal.WebApp.Models;
using Kowmal.WebApp.Services.Interfaces;
using MediatR;

namespace Kowmal.WebApp.Features.UpdatePhotosStore;

public class UpdatePhotosStore : INotificationHandler<PhotosUploaded>
{
    private readonly IPostService _postService;
    private readonly IFileClient _fileClient;

    public UpdatePhotosStore(IPostService postService, IFileClient fileClient)
    {
        _postService = postService;
        _fileClient = fileClient;
    }

    public async Task Handle(PhotosUploaded notification, CancellationToken cancellationToken)
    {
        var posts = await _postService.GetPublishedPostsAsync(cancellationToken);

        var photosStore = new PhotosStore(posts);
        
        var options = new JsonSerializerOptions {
            PropertyNamingPolicy = new LowerCaseNamingPolicy(),
        };

        var json = JsonSerializer.Serialize(photosStore, options);
        
        var utf8 = new UTF8Encoding();
        byte[] content = utf8.GetBytes(json);

        await _fileClient.UploadFileAsync(content, "photos.json", cancellationToken);
    }

    private class PhotosStore
    {
        public DateTime UpdatedAt { get; init; }
        public List<PublishedPost> Posts { get; init; } 

        public PhotosStore(List<Post> posts)
        {
            UpdatedAt = DateTime.Now;
            Posts = posts.Select(x => 
                new PublishedPost(
                    x.Identifier,
                    x.Name,
                    x.Description,
                    x.Photos.Select(p => new PhotoDetails(p.FilePath, p.Width, p.Height)).ToList(),
                    x.Photos.Select(t => new PhotoDetails(t.ThumbnailPath, t.ThumbWidth, t.ThumbHeight)).ToList())
            ).ToList();
        }
    }
    
    private record PublishedPost(Guid Identifier, string Name, string Description, List<PhotoDetails> Photos, List<PhotoDetails> Thumbnails);
    private record PhotoDetails(string Path, int Width, int Height);
}