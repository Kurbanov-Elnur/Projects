
namespace Project_Work_API.Data.Models.DBModels;

public class Faculty
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; }

    public ICollection<Department> Departments { get; set; } 
    public ICollection<Teacher> Teachers { get; set; } 
}