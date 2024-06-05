using Kowmal.API.Clients.Interfaces;
using Kowmal.API.Models;
using Kowmal.API.Services.Interfaces;
using MediatR;

namespace Kowmal.API.Features.UploadPhotos;

public class UploadPhotosHandler : IRequestHandler<UploadPhotosRequest>
{
    private readonly IPostService _postService;
    private readonly IPhotoConverter _photoConverter;
    private readonly IFileClient _fileClient;

    public UploadPhotosHandler(IPostService postService, IPhotoConverter photoConverter, IFileClient fileClient)
    {
        _postService = postService;
        _photoConverter = photoConverter;
        _fileClient = fileClient;
    }
    public async Task Handle(UploadPhotosRequest request, CancellationToken cancellationToken)
    {
        var post = await _postService.GetPostIncludingPhotosAsync(request.PostId, cancellationToken);
        
        if (post == null)
            throw new Exception("Post not found");

        if (request.Files == null)
            throw new Exception("No files provided");
        
        foreach (var file in request.Files)
        {
            await using var stream = file.OpenReadStream();
             var content = UseBufferedStream(stream);
            
           post.AddPhoto(content, file.FileName);
        }
        
        await _postService.UpdatePostAsync(post, cancellationToken);
    }
    
    public byte[] UseBufferedStream(Stream stream)
    {
        byte[] bytes;
        using (var bufferedStream = new BufferedStream(stream))
        {
            using var memoryStream = new MemoryStream();
            bufferedStream.CopyTo(memoryStream);
            bytes = memoryStream.ToArray();
        }
        return bytes;
    }
}