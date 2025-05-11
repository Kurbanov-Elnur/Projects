namespace Auth.Shared.DTOs;

public record ProductDTO(
    string Name,
    decimal Price,
    byte[] Image
);