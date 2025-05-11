using System.Security.Claims;
using Auth.Domain.Models;

namespace Auth.Domain.Interfaces;

public interface ITokenService
{
    public Task<string> GenerateTokenAsync(User user);
    public Task<string> GenerateRefreshTokenAsync();
    public ClaimsPrincipal GetPrincipalFromToken(string token, bool validateLifetime = false);
    public Task<bool> IsTokenAuthorized(string accessToken);
}