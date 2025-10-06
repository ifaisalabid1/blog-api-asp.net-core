using BlogApi.DTOs;

namespace BlogApi.Core.Interfaces;

public interface IPostService
{
    Task<PostDto?> GetPostAsync(int id);
    Task<IReadOnlyList<PostDto>> GetPostsAsync();
    Task<PostDto> CreatePostAsync(CreatePostDto createPostDto, string authorId);
    Task<PostDto?> UpdatePostAsync(int id, UpdatePostDto updatePostDto, string userId);
    Task<bool> DeletePostAsync(int id, string userId);
    Task<IReadOnlyList<PostDto>> GetPostsByAuthorAsync(string authorId);
}