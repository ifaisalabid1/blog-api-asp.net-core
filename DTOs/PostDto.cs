using System.ComponentModel.DataAnnotations;
using BlogApi.Shared.DTOs;

namespace BlogApi.DTOs;

public class CreatePostDto
{
    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Content { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Summary { get; set; }
}

public class UpdatePostDto
{
    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Content { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Summary { get; set; }

    public bool IsPublished { get; set; }
}

public class PostDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string? Summary { get; set; }
    public bool IsPublished { get; set; }
    public DateTime? PublishedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string AuthorId { get; set; } = string.Empty;
    public UserDto Author { get; set; } = null!;
    public int CommentCount { get; set; }
}