namespace Auth.Shared;

public class BasketProductDto
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal ProductPrice { get; set; }
    public int ProductQuantity { get; set; }
    public byte[] ProductImage { get; set; }
}