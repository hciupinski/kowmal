using Microsoft.AspNetCore.Components.Forms;

namespace Kowmal.WebApp.ViewModels;

public class PostViewModel
{
        public Guid? Identifier { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<string> UploadedFileNames { get; set; } = new List<string>();
        public List<IBrowserFile>? NewFiles { get; set; }
}