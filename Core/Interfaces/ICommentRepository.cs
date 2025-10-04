using BlogApi.Core.Entities;

namespace BlogApi.Core.Interfaces;

public interface ICommentRepository : IRepository<Comment>
{
    Task<IReadOnlyList<Comment>> GetCommentsByPostAsync(int postId);
    Task<IReadOnlyList<Comment>> GetCommentsWithDetailsAsync(int postId);
}