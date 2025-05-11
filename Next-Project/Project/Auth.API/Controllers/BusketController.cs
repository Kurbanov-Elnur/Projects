using Asp.Versioning;
using Auth.Domain.Interfaces;
using Auth.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class BusketController : ControllerBase
{
    private readonly IProductService _productService;

    public BusketController(IProductService productService)
    {
        this._productService = productService;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> CreateProduct(IFormFile image, string name, string price)
    {
        if (image == null || image.Length == 0)
            return BadRequest("Файл не выбран");

        if (string.IsNullOrEmpty(price) || !decimal.TryParse(price, out var parsedPrice))
            return BadRequest("Неверная цена");

        using var memoryStream = new MemoryStream();
        await image.CopyToAsync(memoryStream);

        var createdProduct = await _productService.CreateProductAsync(new ProductDTO(
            name,
            parsedPrice,
            memoryStream.ToArray()
        ));

        return Ok(createdProduct);
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _productService.GetProductsAsync();
        return Ok(products);
    }
}