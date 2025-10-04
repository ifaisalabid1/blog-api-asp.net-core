namespace BlogApi.Core.Entities;

public class Comment : BaseEntity
{
    public string Content { get; set; } = string.Empty;
    public bool IsApproved { get; set; } = false;

    public string AuthorId { get; set; } = string.Empty;
    public int PostId { get; set; }

    public virtual User Author { get; set; } = null!;
    public virtual Post Post { get; set; } = null!;
}