namespace Auth.Shared.DTOs.Responses;

public record PostResponseDto(
    string Message,
    int Code = 200
);