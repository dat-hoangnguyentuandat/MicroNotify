using MicroNotify.Identity.Domain.Entities;

namespace MicroNotify.Identity.Application.Services
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
