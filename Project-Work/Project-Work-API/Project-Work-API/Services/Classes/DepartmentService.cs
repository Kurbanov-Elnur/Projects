
using Microsoft.EntityFrameworkCore;
using Project_Work_API.Data.Contexts;
using Project_Work_API.Data.Models.DBModels;
using Project_Work_API.Data.Models.DTO;
using Project_Work_API.Services.Interfaces;

namespace Project_Work_API.Services.Classes;

public class DepartmentService : IDepartmentService
{
    private readonly AppDBContext _appDBContext;

    public DepartmentService(AppDBContext appDBContext)
    {
        _appDBContext = appDBContext;
    }

    public async Task AddDepartment(DepartmentDTO newDepartmentData)
    {
        try
        {
            var facultyID = await _appDBContext.Faculties
                .Where(f => f.Name == newDepartmentData.FacultyName)
                .Select(f => f.Id)
                .FirstOrDefaultAsync();

            if (facultyID == null)
                throw new Exception($"Faculty with name {newDepartmentData.FacultyName} was not found.");

            var newDepartment = new Department()
            {
                Name = newDepartmentData.Name,
                FacultyId = facultyID
            };

            await _appDBContext.Departments.AddAsync(newDepartment);
            await _appDBContext.SaveChangesAsync();
        }
        catch (System.Exception)
        {
            throw;
        }
    }
}