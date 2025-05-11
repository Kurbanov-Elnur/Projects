using System.Security.Claims;
using Auth.Domain.Interfaces;
using Auth.Domain.Models;
using Auth.Persistence.Contexts;
using Auth.Shared.DTOs;
using Auth.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using static BCrypt.Net.BCrypt;

namespace Auth.Application.Services;

public class AuthService : IAuthService
{
    private readonly AuthContext _context;
    private readonly ITokenService _tokenService;

    public AuthService(AuthContext context, ITokenService tokenService)
    {
        this._context = context;
        this._tokenService = tokenService;
    }

    public async Task<AccessInfoDto> SignInAsync(SignInDto user)
    {
        var foundUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username);

        if (foundUser == null)
            throw new AuthException(AuthErrorTypes.UserNotFound, "User not found");

        var userRole = await _context.UserRoles
            .Include(r => r.AppRole)
            .FirstOrDefaultAsync(r => r.UserId == foundUser.Id);

        if (!Verify(user.Password, foundUser.Password))
            throw new AuthException(AuthErrorTypes.InvalidCredentials, "Invalid credentials");

        var tokenData = new AccessInfoDto(
            foundUser.Username,
            await _tokenService.GenerateTokenAsync(foundUser),
            await _tokenService.GenerateRefreshTokenAsync(),
            userRole.AppRole.Name,
            DateTime.Now.AddDays(7)
        );

        foundUser.RefreshToken = tokenData.RefreshToken;
        foundUser.RefreshTokenExpiryTime = tokenData.RefreshTokenExpireTime;

        await _context.SaveChangesAsync();

        return tokenData;
    }

    public async Task<User> SignUpAsync(SignUpDto user)
    {
        var newUser = new User
        {
            Name = user.Name,
            Surname = user.Surname,
            Username = user.Username,
            Email = user.Email,
            Password = HashPassword(user.Password)
        };

        var appRoles = AppRoles.Roles;
        var rolesInDb = await _context.AppRoles.ToListAsync();

        foreach (var item in appRoles)
        {
            if (rolesInDb.All(x => x.Name != item))
            {
                await _context.AppRoles.AddAsync(new AppRole { Name = item });
            }
        }

        await _context.SaveChangesAsync();

        var role = _context.AppRoles.FirstOrDefault(r => r.Name == "AppUser");

        var roleToApply = new UserRole()
        {
            RoleId = role.Id,
            UserId = newUser.Id
        };

        await _context.Users.AddAsync(newUser);
        await _context.UserRoles.AddAsync(roleToApply);
        await _context.SaveChangesAsync();

        return newUser;
    }

    public async Task LogOutAsync(TokenDto userTokenInfo)
    {
        if (userTokenInfo is null)
            throw new AuthException(AuthErrorTypes.InvalidRequest, "Invalid client request");

        var principal = _tokenService.GetPrincipalFromToken(userTokenInfo.AccessToken);

        var userId = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        var user = _context.Users.FirstOrDefault(u => u.Id.ToString() == userId);

        user.RefreshToken = null;
        user.RefreshTokenExpiryTime = DateTime.Now;
        await _context.SaveChangesAsync();
    }

    public async Task<AccessInfoDto> RefreshTokenAsync(TokenDto userAccessData)
    {
        if (userAccessData is null)
            throw new AuthException(AuthErrorTypes.InvalidRequest, "Invalid client request");

        var accessToken = userAccessData.AccessToken;
        var refreshToken = userAccessData.RefreshToken;

        var principal = _tokenService.GetPrincipalFromToken(accessToken);

        var userId = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        var user = _context.Users.FirstOrDefault(u => u.Id.ToString() == userId);

        var userRole = await _context.UserRoles
            .Include(r => r.AppRole)
            .FirstOrDefaultAsync(r => r.UserId == user.Id);

        if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            throw new AuthException(AuthErrorTypes.InvalidRequest, "Invalid client request");

        var newAccessToken = await _tokenService.GenerateTokenAsync(user);
        var newRefreshToken = await _tokenService.GenerateRefreshTokenAsync();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

        await _context.SaveChangesAsync();

        return new AccessInfoDto(
            user.Username,
            newAccessToken,
            newRefreshToken,
            userRole.AppRole.Name,
            user.RefreshTokenExpiryTime);
    }
}