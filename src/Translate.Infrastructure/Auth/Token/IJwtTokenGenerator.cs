using System.Security.Claims;

namespace Translate.Infrastructure.Auth.Token
{
    public interface IJwtTokenGenerator
    {
        string GerarToken(string nomeCompleto, string email, IEnumerable<Claim>? listaClaims);
        ClaimsPrincipal? GetInfoTokenExpirado(string? token);
    }
}