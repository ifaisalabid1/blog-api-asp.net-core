namespace BlogApi.Core.Entities;

public class Post : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string? Summary { get; set; }
    public bool IsPublished { get; set; } = false;
    public DateTime? PublishedAt { get; set; }

    public string AuthorId { get; set; } = string.Empty;

    public virtual User Author { get; set; } = null!;
    public virtual ICollection<Comment> Comments { get; set; } = [];
}