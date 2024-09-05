namespace Project_Work_API.Data.Models.DTO;

public record TokenDataDTO
(
    string AccessToken,
    string RefreshToken,
    DateTime RefreshTokenExpireTime
);