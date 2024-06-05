using Microsoft.AspNetCore.Components.Forms;

namespace Kowmal.WebApp.Models;

public record CreatePostRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}