using System.Security.Claims;
using Project_Work_API.Data.Models.DBModels;

namespace Project_Work_API.Services.Interfaces;

public interface ITokenService
{
    public Task<string> GenerateTokenAsync(User user);
    public Task<string> GenerateRefreshTokenAsync();
    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}