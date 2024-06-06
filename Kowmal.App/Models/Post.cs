using System.ComponentModel.DataAnnotations;
using Kowmal.App.Models.Base;

namespace Kowmal.App.Models;

public class Post : Entity
{
    private List<Photo> _photos = new List<Photo>();
    public Guid Identifier { get; init; }
    
    [Required]
    [MaxLength(150)]
    public string Name { get; private set; } = string.Empty;
    [MaxLength(400)]
    public string Description { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; init; }
    public bool IsPublished { get; private set; }
    public virtual IReadOnlyCollection<Photo> Photos => _photos;

    public Post()
    {
        Identifier = Guid.NewGuid();
        CreatedAt = DateTime.Now;
    }
    
    public Post AddPhoto(byte[] content, string originalFileName)
    {
        var photo = new Photo(this, originalFileName, content);
        _photos.Add(photo);
        return this;
    }

    public Post SetDetails(string name, string description)
    {
        Name = name;
        Description = description;
        return this;
    }

    public Post SetIsPublished()
    {
        IsPublished = true;
        return this;
    }

    public Post ClearContent()
    {
        _photos.ForEach(photo => photo.Content = null);
        return this;
    }
}