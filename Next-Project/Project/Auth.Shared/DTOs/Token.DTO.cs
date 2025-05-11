namespace Auth.Shared.DTOs;

public record TokenDto(
    string AccessToken,
    string RefreshToken
);