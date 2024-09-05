
using Microsoft.AspNetCore.Mvc;
using Project_Work_API.Data.Models.DTO;
using Project_Work_API.Services.Interfaces;

namespace Project_Work_API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EmailController : ControllerBase
{
    private readonly IEmailService _emailService;

    public EmailController(IEmailService emailService)
    {
        _emailService = emailService;
    }

    [HttpPost("SendVerificationCode")]
    public async Task<IActionResult> SendVerificationCode([FromBody] string email)
    {
        var res = await _emailService.SendVerificationCodeAsync(email);

        if (res)
            return Ok("The verification code has been successfully sent");
        else
            return BadRequest("User not found!");
    }

    [HttpPost("VerifyEmail")]
    public async Task<IActionResult> VerifyEmail([FromBody] EmailVerifyDataDTO verifyData)
    {
        var res = await _emailService.VerifyEmail(verifyData);

        if (res)
            return Ok("The account has been verified");
        else
            return BadRequest("Account verification failed");
    }
}