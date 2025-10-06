using BlogApi.Core.Entities;

namespace BlogApi.Core.Interfaces;

public interface ITokenService
{
    Task<string> CreateToken(User user);
}