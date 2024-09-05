
using Microsoft.AspNetCore.Mvc;
using Project_Work_API.Data.Models.DTO;
using Project_Work_API.Services.Interfaces;
using Project_Work_API.Validators;

namespace Project_Work_API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController : ControllerBase
{

    private readonly SignInUserValidator _signInValidator;
    private readonly IAuthService _authService;

    public AuthController(SignInUserValidator signInValidator, IAuthService authService)
    {
        this._signInValidator = signInValidator;
        this._authService = authService;
    }

    [HttpPost("SignIn")]

    public async Task<IActionResult> SignInAsync([FromBody] SignInUserDTO user)
    {
        var validationResult = _signInValidator.Validate(user);

        if(!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        try
        {
            var res = await _authService.SignInUserAsync(user);

            return Ok(res);
        }
        catch (System.Exception)
        {
            throw;
        }
    }
}