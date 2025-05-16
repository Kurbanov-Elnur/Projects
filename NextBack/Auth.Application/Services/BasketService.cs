using Auth.Domain.Interfaces;
using Auth.Persistence.Contexts;
using Auth.Shared;
using Auth.Shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Auth.Application.Services;

public class BasketService : IBasketService
{
    private readonly AuthContext _context;

    public BasketService(AuthContext context)
    {
        _context = context;
    }

    public async Task<BasketDto> GetBasketAsync(Guid userId)
    {
        var basket = await _context.Baskets
            .Include(b => b.Products)
            .ThenInclude(bp => bp.Product)
            .FirstOrDefaultAsync(b => b.UserId == userId);

        if (basket == null)
            throw new Exception("Basket not found");

        return new BasketDto
        {
            Id = basket.Id,
            UserId = basket.UserId,
            TotalPrice = basket.TotalPrice,
            Products = basket.Products.Select(bp => new BasketProductDto
            {
                ProductId = bp.ProductId,
                ProductName = bp.Product.Name,
                ProductPrice = bp.Product.Price,
                ProductQuantity = bp.Quantity,
                ProductImage = bp.Product.Image
            }).ToList()
        };
    }

    public async Task<bool> AddProductToBasketAsync(Guid userId, Guid productId, int quantity)
    {
        var basket = await _context.Baskets.FirstOrDefaultAsync(b => b.UserId == userId);
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);

        if (basket == null || product == null)
            return false;

        var existingProduct = await _context.BasketProducts
            .FirstOrDefaultAsync(bp => bp.BasketId == basket.Id && bp.ProductId == productId);

        if (existingProduct != null)
        {
            existingProduct.Quantity += quantity;
            basket.TotalPrice += product.Price * quantity;
        }
        else
        {
            var newProductToBasket = new BasketProduct
            {
                ProductId = productId,
                Product = product,
                BasketId = basket.Id,
                Basket = basket,
                Quantity = quantity
            };
            basket.TotalPrice += product.Price * quantity;

            _context.BasketProducts.Add(newProductToBasket);
        }

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> IncreaseProductQuantityAsync(Guid userId, Guid productId)
    {
        var basket = await _context.Baskets.FirstOrDefaultAsync(b => b.UserId == userId);
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);

        if (basket == null || product == null)
            return false;

        var existingProduct = await _context.BasketProducts
            .FirstOrDefaultAsync(bp => bp.BasketId == basket.Id && bp.ProductId == productId);

        if (existingProduct == null)
            return false;

        existingProduct.Quantity += 1;
        basket.TotalPrice += product.Price;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DecreaseProductQuantityAsync(Guid userId, Guid productId)
    {
        var basket = await _context.Baskets.FirstOrDefaultAsync(b => b.UserId == userId);
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);

        if (basket == null || product == null)
            return false;

        var existingProduct = await _context.BasketProducts
            .FirstOrDefaultAsync(bp => bp.BasketId == basket.Id && bp.ProductId == productId);

        if (existingProduct == null || existingProduct.Quantity <= 0)
            return false;

        existingProduct.Quantity -= 1;
        basket.TotalPrice -= product.Price;

        if (existingProduct.Quantity == 0)
        {
            _context.BasketProducts.Remove(existingProduct);
        }

        await _context.SaveChangesAsync();
        return true;
    }
}