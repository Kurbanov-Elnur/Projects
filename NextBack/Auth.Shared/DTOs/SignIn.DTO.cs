namespace Auth.Shared.DTOs;

public record SignInDto(
    string Username,
    string Password
);