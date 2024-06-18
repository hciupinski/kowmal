using Microsoft.AspNetCore.Components.Forms;

namespace Kowmal.WebApp.ViewModels;

public class CreatePostViewModel
{
        public Guid? Identifier { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<IBrowserFile>? Files { get; set; }
}