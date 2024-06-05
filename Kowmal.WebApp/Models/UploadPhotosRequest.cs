using Microsoft.AspNetCore.Components.Forms;

namespace Kowmal.WebApp.Models;

public record UploadPhotosRequest
{
    public Guid Identifier { get; set; }
    public List<IBrowserFile>? Files { get; set; }
}