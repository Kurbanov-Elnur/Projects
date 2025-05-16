using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using Auth.Application.Validators;
using Auth.Domain.Interfaces;
using Auth.Shared.DTOs;
using Auth.Shared.DTOs.Responses;
using Auth.Shared.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace Auth.API.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthController : ControllerBase
{
    private readonly SignInValidator _signInValidator;
    private readonly SignUpValidator _signUpValidator;
    private readonly IAuthService _authService;

    public AuthController(SignInValidator signInValidator, SignUpValidator signUpValidator,
        IAuthService authService)
    {
        this._signInValidator = signInValidator;
        this._signUpValidator = signUpValidator;
        this._authService = authService;
    }

    [AllowAnonymous]
    [HttpPost("SignIn")]
    public async Task<IActionResult> SignInAsync([FromBody] SignInDto user)
    {
        var validationResult = _signInValidator.Validate(user);

        if (!validationResult.IsValid)
        {
            throw new AuthException(AuthErrorTypes.InvalidCredentials,
                JsonConvert.SerializeObject(validationResult.Errors, Formatting.Indented));
        }

        var res = await _authService.SignInAsync(user);

        Response.Cookies.Append("accessToken", res.AccessToken);
        Response.Cookies.Append("refreshToken", res.RefreshToken);

        return Ok(new SignInResponseDto(res.UserName));
    }

    [AllowAnonymous]
    [HttpPost("SignUp")]
    public async Task<IActionResult> SignUpAsync([FromBody] SignUpDto user)
    {
        var validationResult = _signUpValidator.Validate(user);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var res = await _authService.SignUpAsync(user);

        return Ok(new PostResponseDto("Registration completed"));
    }

    [Authorize]
    [HttpPost("Logout")]
    public async Task<IActionResult> LogoutAsync()
    {
        if (!Request.Cookies.ContainsKey("accessToken") && !Request.Cookies.ContainsKey("refreshToken"))
        {
            return Ok(new PostResponseDto("Please log in for logging out", 404));
        }

        var logoutInfo = new TokenDto(Request.Cookies["accessToken"], Request.Cookies["refreshToken"]);
        await _authService.LogOutAsync(logoutInfo);

        Response.Cookies.Delete("accessToken");
        Response.Cookies.Delete("refreshToken");

        return Ok(new PostResponseDto("Logged out successfully", 200));
    }

    [HttpPost("Refresh")]
    public async Task<IActionResult> RefreshTokenAsync(TokenDto tokenDto)
    {
        var newToken = await _authService.RefreshTokenAsync(tokenDto);

        if (newToken is null)
            return BadRequest("Invalid token");

        var res = new RefreshDto(newToken.AccessToken, newToken.RefreshToken);
        return Ok(res);
    }
}