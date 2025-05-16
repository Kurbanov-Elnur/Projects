namespace Auth.Shared.DTOs;

public record AccessInfoDto(
    string UserName,
    string AccessToken,
    string RefreshToken,
    string Role,
    DateTime RefreshTokenExpireTime
);