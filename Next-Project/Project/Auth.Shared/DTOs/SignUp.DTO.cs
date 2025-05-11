namespace Auth.Shared.DTOs;

public record SignUpDto(
    string Name,
    string Surname,
    string Username,
    string Email,
    string Password,
    string ConfirmPassword
);