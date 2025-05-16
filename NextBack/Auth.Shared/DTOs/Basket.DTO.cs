namespace Auth.Shared.DTOs;

public class BasketDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public List<BasketProductDto> Products { get; set; }
    public decimal? TotalPrice { get; set; }
}