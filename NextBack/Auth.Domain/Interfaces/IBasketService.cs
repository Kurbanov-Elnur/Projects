using Auth.Shared.DTOs;

namespace Auth.Domain.Interfaces;

public interface IBasketService
{
    public Task<BasketDto> GetBasketAsync(Guid userId);
    public Task<bool> AddProductToBasketAsync(Guid userId, Guid productId, int quantity);
    public Task<bool> IncreaseProductQuantityAsync(Guid userId, Guid productId);
    public Task<bool> DecreaseProductQuantityAsync(Guid userId, Guid productId);
}