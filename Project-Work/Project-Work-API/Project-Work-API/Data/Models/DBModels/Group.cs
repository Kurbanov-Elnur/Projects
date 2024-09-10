
namespace Project_Work_API.Data.Models.DBModels;

public class Group
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } 

    public string? TeacherId { get; set; }
    public Teacher? Teacher { get; set; }

    public string? DepartmentId { get; set; }
    public Department? Department { get; set; }

    public ICollection<Student> Students { get; set; } 
}