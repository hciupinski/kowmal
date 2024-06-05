using MediatR;

namespace Kowmal.API.Features.UploadPhotos;

public class UploadPhotosRequest : IRequest
{
    public Guid PostId { get; set; }
    public IEnumerable<IFormFile>? Files { get; set; }
}