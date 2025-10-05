using BlogApi.Core.Entities;
using BlogApi.Core.Interfaces;
using BlogApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Infrastructure.Repositories;

public class PostRepository(AppDbContext context) : Repository<Post>(context), IPostRepository
{
    public async Task<IReadOnlyList<Post>> GetPostsByAuthorAsync(string authorId)
    {
        return await _context.Posts
            .Include(p => p.Author)
            .Where(p => p.AuthorId == authorId)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Post>> GetPostsWithAuthorAsync()
    {
        return await _context.Posts
           .Include(p => p.Author)
           .Include(p => p.Comments)
           .OrderByDescending(p => p.CreatedAt)
           .ToListAsync();
    }

    public async Task<Post?> GetPostWithDetailsAsync(int id)
    {
        return await _context.Posts
            .Include(p => p.Author)
            .Include(p => p.Comments)
                .ThenInclude(c => c.Author)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
}