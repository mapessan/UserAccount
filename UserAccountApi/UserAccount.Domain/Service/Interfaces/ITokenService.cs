using UserAccount.Domain.Entity;

namespace UserAccount.Domain.Service.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user);
}

