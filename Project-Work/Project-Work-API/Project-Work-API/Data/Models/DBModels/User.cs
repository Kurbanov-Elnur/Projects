using Project_Work_API.Constants;

namespace Project_Work_API.Data.Models.DBModels;

public class User
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; } 
    public string Password { get; set; } 
    public Role Role { get; set; } 
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }

    public ICollection<Teacher> Teachers { get; set; }
    public ICollection<Student> Students { get; set; }
}