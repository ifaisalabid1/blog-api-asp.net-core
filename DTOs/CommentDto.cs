using System.ComponentModel.DataAnnotations;
using BlogApi.Shared.DTOs;

namespace BlogApi.DTOs;

public class CreateCommentDto
{
    [Required]
    [StringLength(1000)]
    public string Content { get; set; } = string.Empty;

    [Required]
    public int PostId { get; set; }
}

public class UpdateCommentDto
{
    [Required]
    [StringLength(1000)]
    public string Content { get; set; } = string.Empty;

    public bool IsApproved { get; set; }
}

public class CommentDto
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public bool IsApproved { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string AuthorId { get; set; } = string.Empty;
    public UserDto Author { get; set; } = null!;
    public int PostId { get; set; }
}