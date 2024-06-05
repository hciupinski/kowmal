using System.ComponentModel.DataAnnotations;
using Kowmal.API.Models.Base;

namespace Kowmal.API.Models;

public class Photo : Entity
{
    [Required]
    [MaxLength(250)]
    public string FileName { get; init; }
    
    [Required]
    [MaxLength(4)]
    public string FileExtension { get; init; }
    public DateTime CreatedAt { get; init; }
    public int Width { get; private set; }
    public int Height { get; private set; }
    
    public int ThumbWidth { get; private set; }
    public int ThumbHeight { get; private set; }
    
    public byte[]? Content { get; set; }
    
    public Post Post { get; set; }
    public int PostId { get; set; }

    public string FilePath => GetFullFilePath();

    public string ThumbnailPath => GetFullFilePath(true);
    
    private Photo(){}

    public Photo(Post post, string originalFileName, byte[] content)
    {
        Post = post;
        PostId = post.SId;
        FileName = Guid.NewGuid().ToString();
        FileExtension = Path.GetExtension(originalFileName);
        Content = content;
        CreatedAt = DateTime.Now;
    }
    
    public Photo SetDetails(int width, int height, int thumbWidth, int thumbHeight)
    {
        Width = width;
        Height = height;
        ThumbWidth = thumbWidth;
        ThumbHeight = thumbHeight;
        return this;
    }

    private string GetFullFilePath(bool isThumb = false)
    {
        var fileName = !isThumb ? 
            string.Concat(FileName, FileExtension) :
            string.Concat(FileName, "_thumb", FileExtension);

        return Path.Combine("photos", Post.Identifier.ToString(), fileName);
    }
}