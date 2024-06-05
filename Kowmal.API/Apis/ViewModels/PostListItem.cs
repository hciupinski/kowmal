namespace Kowmal.API.Apis.ViewModels;

public class PostListItem
{
    public int SId { get; set; }
    public Guid Identifier { get; set;}
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int PhotosCount { get; set; }
    public bool IsPublished { get; set; }
}