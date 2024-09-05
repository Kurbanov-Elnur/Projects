using Project_Work_API.Data.Models.DTO;

namespace Project_Work_API.Services.Interfaces;

public interface IAuthService
{
    public Task<TokenDataDTO> SignInUserAsync(SignInUserDTO user);
    public Task<TokenDataDTO> RefreshTokenAsync(UserTokenInfoDTO userAccessData);
}