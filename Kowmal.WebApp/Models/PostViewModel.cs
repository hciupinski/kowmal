namespace Kowmal.WebApp.Models;

public class PostViewModel
{
    public int SId { get; set; }
    public Guid Identifier { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public bool IsPublished { get; set; }
    public List<PhotoViewModel> Photos { get; set; } = new List<PhotoViewModel>();
}
