using System.Security.Claims;
using Asp.Versioning;
using Auth.Domain.Interfaces;
using Auth.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class BasketController : ControllerBase
{
    private readonly IBasketService _basketService;
    private readonly ITokenService _tokenService;

    public BasketController(IBasketService basketService, ITokenService tokenService)
    {
        _basketService = basketService;
        _tokenService = tokenService;
    }

    [HttpGet]
    public async Task<IActionResult> GetBasket()
    {
        var claims = _tokenService.GetPrincipalFromToken(Request.Cookies["accessToken"]);
        var id = claims.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var basket = await _basketService.GetBasketAsync(Guid.Parse(id));
        return Ok(basket);
    }

    [HttpPost]
    public async Task<IActionResult> AddProductToBasket(Guid productId, int quantity)
    {
        var claims = _tokenService.GetPrincipalFromToken(Request.Cookies["accessToken"]);
        var id = claims.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var res = await _basketService.AddProductToBasketAsync(Guid.Parse(id), productId, quantity);

        if (res)
            return Ok("Product successfully added to basket.");
        else
            return NotFound("Basket or product not found.");
    }

    [HttpPost("increase")]
    public async Task<IActionResult> IncreaseProductQuantity(Guid productId)
    {
        var claims = _tokenService.GetPrincipalFromToken(Request.Cookies["accessToken"]);
        var id = claims.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var res = await _basketService.IncreaseProductQuantityAsync(Guid.Parse(id), productId);

        if (res)
            return Ok("Product quantity increased.");
        else
            return NotFound("Basket or product not found.");
    }

    [HttpPost("decrease")]
    public async Task<IActionResult> DecreaseProductQuantity(Guid productId)
    {
        var claims = _tokenService.GetPrincipalFromToken(Request.Cookies["accessToken"]);
        var id = claims.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var res = await _basketService.DecreaseProductQuantityAsync(Guid.Parse(id), productId);

        if (res)
            return Ok("Product quantity decreased.");
        else
            return NotFound("Basket or product not found or quantity already zero.");
    }
}