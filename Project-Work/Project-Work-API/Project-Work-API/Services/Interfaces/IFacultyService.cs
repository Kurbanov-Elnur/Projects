
using Project_Work_API.Data.Models.DTO;

namespace Project_Work_API.Services.Interfaces;

public interface IFacultyService
{
    public Task AddFaculty(FacultyDTO newFacultyData);
}