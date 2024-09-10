
namespace Project_Work_API.Data.Models.DBModels;
public class Teacher
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string UserId { get; set; }
    public User User { get; set; }

    public string? FacultyId { get; set; }
    public Faculty? Faculty { get; set; }

    public ICollection<Group> Groups { get; set; } 
}