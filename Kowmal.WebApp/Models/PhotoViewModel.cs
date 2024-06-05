namespace Kowmal.WebApp.Models;

public class PhotoViewModel
{
    public int SId { get; set; }
    public string FileName { get; set; }
    public string FileExtension { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public PostViewModel PostViewModel { get; set; }
    public int PostId { get; set; }
}