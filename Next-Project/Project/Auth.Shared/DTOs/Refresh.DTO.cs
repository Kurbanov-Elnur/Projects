namespace Auth.Shared.DTOs;

public record RefreshDto(
    string AccessToken,
    string RefreshToken
);