using geolocation.Entities;

namespace geolocation.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
        int GetUserIdFromToken(HttpRequest request);
    }
}