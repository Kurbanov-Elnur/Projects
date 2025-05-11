using Auth.Domain.Interfaces;
using Auth.Domain.Models;
using Auth.Persistence.Contexts;
using Auth.Shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Auth.Application.Services;

public class ProductService : IProductService
{
    private readonly AuthContext _context;
    
    public ProductService(AuthContext context)
    {
        _context = context;
    }
    
    public async Task<Product> CreateProductAsync(ProductDTO product)
    {
        var newProduct = new Product
        {
            Name = product.Name,
            Price = product.Price,
            Image = product.Image, 
        };

        _context.Products.Add(newProduct);
        await _context.SaveChangesAsync();

        return newProduct;
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public Task<Product> GetProductByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}