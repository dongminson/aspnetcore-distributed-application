using user.Entities;

namespace user.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);

        int GetUserIdFromToken(HttpRequest request);
    }
}