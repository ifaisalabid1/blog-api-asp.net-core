using System.Globalization;
using BlogApi.Core.Entities;

namespace BlogApi.Core.Interfaces;

public interface IPostRepository : IRepository<Post>
{
    Task<IReadOnlyList<Post>> GetPostsWithAuthorAsync();

    Task<Post?> GetPostWithDetailsAsync(int id);

    Task<IReadOnlyList<Post>> GetPostsByAuthorAsync(string authorId);
}