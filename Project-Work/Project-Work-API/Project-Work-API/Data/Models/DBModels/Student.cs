
namespace Project_Work_API.Data.Models.DBModels;

public class Student
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string UserId { get; set; }
    public User User { get; set; }
    
    public string GroupId { get; set; }
    public Group Group { get; set; }
}