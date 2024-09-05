
namespace Project_Work_API.Data.Models.DBModels;

public class Department
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } 

    public string FacultyId { get; set; }
    public Faculty Faculty { get; set; }

    public ICollection<Group> Groups { get; set; } 
}