using AutoMapper;
using BlogApi.Core.Entities;
using BlogApi.Core.Interfaces;
using BlogApi.DTOs;

namespace BlogApi.Infrastructure.Services;

public class PostService(IPostRepository postRepository, IMapper mapper) : IPostService
{
    private readonly IPostRepository _postRepository = postRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<PostDto> CreatePostAsync(CreatePostDto createPostDto, string authorId)
    {
        var post = new Post
        {
            Title = createPostDto.Title,
            Content = createPostDto.Content,
            Summary = createPostDto.Summary,
            AuthorId = authorId,
            CreatedAt = DateTime.Now
        };

        var createdPost = await _postRepository.AddAsync(post);

        return _mapper.Map<PostDto>(createdPost);
    }

    public async Task<bool> DeletePostAsync(int id, string userId)
    {
        var post = await _postRepository.GetByIdAsync(id);

        if (post == null || post.AuthorId != userId)
            return false;

        await _postRepository.DeleteAsync(post);
        return true;
    }

    public async Task<PostDto?> GetPostAsync(int id)
    {
        var post = await _postRepository.GetPostWithDetailsAsync(id);
        return post == null ? null : _mapper.Map<PostDto>(post);
    }

    public async Task<IReadOnlyList<PostDto>> GetPostsAsync()
    {
        var posts = await _postRepository.GetPostsWithAuthorAsync();
        return _mapper.Map<IReadOnlyList<PostDto>>(posts);
    }

    public async Task<IReadOnlyList<PostDto>> GetPostsByAuthorAsync(string authorId)
    {
        var posts = await _postRepository.GetPostsByAuthorAsync(authorId);

        return _mapper.Map<IReadOnlyList<PostDto>>(posts);
    }

    public async Task<PostDto?> UpdatePostAsync(int id, UpdatePostDto updatePostDto, string userId)
    {
        var post = await _postRepository.GetByIdAsync(id);
        if (post == null || post.AuthorId != userId)
            return null;

        post.Title = updatePostDto.Title;
        post.Content = updatePostDto.Content;
        post.Summary = updatePostDto.Summary;
        post.IsPublished = updatePostDto.IsPublished;
        post.UpdatedAt = DateTime.Now;

        if (updatePostDto.IsPublished && !post.IsPublished)
        {
            post.PublishedAt = DateTime.Now;
        }

        await _postRepository.UpdateAsync(post);
        return _mapper.Map<PostDto>(post);
    }
}