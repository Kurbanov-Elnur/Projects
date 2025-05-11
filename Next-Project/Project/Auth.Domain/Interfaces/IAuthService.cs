using Auth.Domain.Models;
using Auth.Shared.DTOs;

namespace Auth.Domain.Interfaces;

public interface IAuthService
{
    public Task<AccessInfoDto> SignInAsync(SignInDto user);
    public Task<User> SignUpAsync(SignUpDto user);
    public Task<AccessInfoDto> RefreshTokenAsync(TokenDto userAccessData);
    public Task LogOutAsync(TokenDto userTokenInfo);
}