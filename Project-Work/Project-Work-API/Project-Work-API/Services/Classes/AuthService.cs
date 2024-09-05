using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Project_Work_API.Data.Contexts;
using Project_Work_API.Data.Models.DTO;
using Project_Work_API.Services.Interfaces;
using static BCrypt.Net.BCrypt;

namespace Project_Work_API.Services.Classes;

class AuthService : IAuthService
{
    private readonly AppDBContext _context;
    private readonly ITokenService _tokenService;
    private readonly IEmailService _emailService;

    public AuthService(AppDBContext context, ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    public async Task<TokenDataDTO> SignInUserAsync(SignInUserDTO user)
    {
        try
        {
            var foundUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);

            if (foundUser == null)
            {
                throw new Exception("User not found");
            }

            if (!Verify(user.Password, foundUser.Password))
            {
                throw new Exception("Invalid password");
            }

            var tokenData = new TokenDataDTO
            (
                AccessToken: await _tokenService.GenerateTokenAsync(foundUser),
                RefreshToken: await _tokenService.GenerateRefreshTokenAsync(),
                RefreshTokenExpireTime: DateTime.Now.AddDays(1)
            );

            foundUser.RefreshToken = tokenData.RefreshToken;
            foundUser.RefreshTokenExpiryTime = tokenData.RefreshTokenExpireTime;

            await _context.SaveChangesAsync();

            return tokenData;
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public async Task<TokenDataDTO> RefreshTokenAsync(UserTokenInfoDTO userAccessData)
    {
        if (userAccessData is null)
            throw new Exception("Invalid client request");

        var accessToken = userAccessData.AccessToken;
        var refreshToken = userAccessData.RefreshToken;

        var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);

        var email = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

        var user = _context.Users.FirstOrDefault(u => u.Email == email);

        if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            throw new Exception("Invalid client request");

        var newTokenData = new TokenDataDTO
        (
            AccessToken: await _tokenService.GenerateTokenAsync(user),
            RefreshToken: await _tokenService.GenerateRefreshTokenAsync(),
            RefreshTokenExpireTime: DateTime.Now.AddDays(1)
        );

        user.RefreshToken = newTokenData.RefreshToken;
        user.RefreshTokenExpiryTime = newTokenData.RefreshTokenExpireTime;

        await _context.SaveChangesAsync();

        return newTokenData;
    }
}