namespace Auth.Domain.Models;

public class Basket
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public User User { get; set; }

    public List<BasketProduct> Products { get; set; } = new();
    public decimal? TotalPrice { get; set; }
}