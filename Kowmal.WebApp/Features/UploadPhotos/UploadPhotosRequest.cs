using MediatR;
using Microsoft.AspNetCore.Components.Forms;

namespace Kowmal.WebApp.Features.UploadPhotos;

public class UploadPhotosRequest : IRequest
{
    public Guid PostId { get; set; }
    public IEnumerable<IBrowserFile>? Files { get; set; }
}