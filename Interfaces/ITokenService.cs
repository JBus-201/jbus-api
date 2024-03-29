using API.Entities;

namespace API.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user, int passengerId);
        string GetToken();
    }
}