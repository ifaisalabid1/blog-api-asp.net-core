using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using BlogApi.DTOs;
using BlogApi.Core.Entities;
using BlogApi.Core.Interfaces;

namespace BlogApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentRepository _commentRepository;
    private readonly IPostRepository _postRepository;
    private readonly IMapper _mapper;

    public CommentsController(
        ICommentRepository commentRepository,
        IPostRepository postRepository,
        IMapper mapper)
    {
        _commentRepository = commentRepository;
        _postRepository = postRepository;
        _mapper = mapper;
    }

    [HttpGet("post/{postId}")]
    [AllowAnonymous]
    public async Task<ActionResult<IReadOnlyList<CommentDto>>> GetCommentsForPost(int postId)
    {
        var comments = await _commentRepository.GetCommentsByPostAsync(postId);
        return Ok(_mapper.Map<IReadOnlyList<CommentDto>>(comments));
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<CommentDto>> CreateComment(CreateCommentDto createCommentDto)
    {
        var post = await _postRepository.GetByIdAsync(createCommentDto.PostId);
        if (post == null)
        {
            return NotFound("Post not found");
        }

        var authorId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(authorId))
        {
            return Unauthorized();
        }

        var comment = new Comment
        {
            Content = createCommentDto.Content,
            PostId = createCommentDto.PostId,
            AuthorId = authorId,
            CreatedAt = DateTime.UtcNow
        };

        var createdComment = await _commentRepository.AddAsync(comment);
        var commentDto = _mapper.Map<CommentDto>(createdComment);

        return CreatedAtAction(nameof(GetCommentsForPost), new { postId = comment.PostId }, commentDto);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<CommentDto>> UpdateComment(int id, UpdateCommentDto updateCommentDto)
    {
        var comment = await _commentRepository.GetByIdAsync(id);
        if (comment == null)
        {
            return NotFound();
        }

        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (comment.AuthorId != userId)
        {
            return Forbid();
        }

        comment.Content = updateCommentDto.Content;
        comment.IsApproved = updateCommentDto.IsApproved;
        comment.UpdatedAt = DateTime.UtcNow;

        await _commentRepository.UpdateAsync(comment);
        return Ok(_mapper.Map<CommentDto>(comment));
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult> DeleteComment(int id)
    {
        var comment = await _commentRepository.GetByIdAsync(id);
        if (comment == null)
        {
            return NotFound();
        }

        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (comment.AuthorId != userId)
        {
            return Forbid();
        }

        await _commentRepository.DeleteAsync(comment);
        return NoContent();
    }
}