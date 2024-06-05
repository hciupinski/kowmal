using Kowmal.API.Clients.Interfaces;
using Kowmal.API.Features.UpdatePhotosStore;
using Kowmal.API.Services.Interfaces;
using MediatR;

namespace Kowmal.API.Features.PublishPost;

public class PublishPostHandler : IRequestHandler<PublishPostRequest>
{
    private readonly IPostService _postService;
    private readonly IPhotoConverter _photoConverter;
    private readonly IFileClient _fileClient;
    private readonly IMediator _mediator;

    public PublishPostHandler(IPostService postService, IPhotoConverter photoConverter, IFileClient fileClient, IMediator mediator)
    {
        _postService = postService;
        _photoConverter = photoConverter;
        _fileClient = fileClient;
        _mediator = mediator;
    }

    public async Task Handle(PublishPostRequest request, CancellationToken cancellationToken)
    {
        var post = await _postService.GetPostIncludingPhotosAsync(request.Identifier, cancellationToken);
        
        if (post == null)
            throw new Exception("Post not found");
        
        if (post.IsPublished)
            throw new Exception("Post already published");
        
        foreach (var photo in post.Photos)
        {
            var stream = new MemoryStream(photo.Content!);
        
            var watermarked = _photoConverter.AddWatermark(stream);
            var scaled = _photoConverter.ScaleImage(watermarked, 1200, 1200);
            var thumb = _photoConverter.ScaleImage(watermarked, 300, 300);
            
            photo.SetDetails(scaled.Width, scaled.Height, thumb.Width, thumb.Height);
        
            await _fileClient.UploadFileAsync(scaled.Content, photo.FilePath, cancellationToken);
            await _fileClient.UploadFileAsync(thumb.Content, photo.ThumbnailPath, cancellationToken);
        }

        post.SetIsPublished().ClearContent();
        await _postService.UpdatePostAsync(post, cancellationToken);

        await _mediator.Publish(new PhotosUploaded(), cancellationToken);
    }
}