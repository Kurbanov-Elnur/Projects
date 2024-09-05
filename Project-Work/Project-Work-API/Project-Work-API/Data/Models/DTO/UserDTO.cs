
using Project_Work_API.Constants;

namespace Project_Work_API.Data.Models.DTO;

public record UserDTO
(
    string Name,
    string Surname,
    string Email,
    string Password,
    Role Role = Role.Guest
);