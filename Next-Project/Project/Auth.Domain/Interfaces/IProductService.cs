using Auth.Domain.Models;
using Auth.Shared.DTOs;

namespace Auth.Domain.Interfaces;

public interface IProductService
{
    public Task<IEnumerable<Product>> GetProductsAsync();
    public Task<Product> GetProductByIdAsync(Guid id);
    public Task<Product> CreateProductAsync(ProductDTO product);
}