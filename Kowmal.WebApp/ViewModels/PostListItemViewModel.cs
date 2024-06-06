namespace Kowmal.WebApp.ViewModels;

public class PostListItemViewModel
{
    public int SId { get; set; }
    public Guid Identifier { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public bool IsPublished { get; set; }
    public int PhotosCount { get; set; }
}
