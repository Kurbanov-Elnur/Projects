namespace Auth.Shared.DTOs;

public class BasketDTO
{
    public Guid[] ProductId { get; set; }
    public ProductDTO[] Products { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
}