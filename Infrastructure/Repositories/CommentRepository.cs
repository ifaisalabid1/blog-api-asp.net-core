using BlogApi.Core.Entities;
using BlogApi.Core.Interfaces;
using BlogApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Infrastructure.Repositories;

public class CommentRepository(AppDbContext context) : Repository<Comment>(context), ICommentRepository
{
    public async Task<IReadOnlyList<Comment>> GetCommentsByPostAsync(int postId)
    {
        return await _context.Comments
            .Include(c => c.Author)
            .Where(c => c.PostId == postId && c.IsApproved)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Comment>> GetCommentsWithDetailsAsync(int postId)
    {
        return await _context.Comments
            .Include(c => c.Author)
            .Include(c => c.Post)
            .Where(c => c.PostId == postId)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();
    }
}