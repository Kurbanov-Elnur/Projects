using Project_Work_API.Data.Models.DTO;

namespace Project_Work_API.Services.Interfaces;

public interface IEmailService
{
    public Task<bool> SendVerificationCodeAsync(string email);
    public Task<bool> VerifyEmail(EmailVerifyDataDTO verifyData);
}