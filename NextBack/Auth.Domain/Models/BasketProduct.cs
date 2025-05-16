using Auth.Domain.Models;

public class BasketProduct
{
    public Guid Id { get; set; } = new Guid();

    public Guid ProductId { get; set; }
    public Product Product { get; set; }

    public Guid BasketId { get; set; } 
    public Basket Basket { get; set; }

    public int Quantity { get; set; }
}