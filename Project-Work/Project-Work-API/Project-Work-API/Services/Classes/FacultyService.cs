
using System.Data.SqlTypes;
using Org.BouncyCastle.Pkix;
using Project_Work_API.Data.Contexts;
using Project_Work_API.Data.Models.DBModels;
using Project_Work_API.Data.Models.DTO;
using Project_Work_API.Services.Interfaces;

namespace Project_Work_API.Services.Classes;

public class FacultyService : IFacultyService
{
    private readonly AppDBContext _appDBContext;

    public FacultyService(AppDBContext appDBContext)
    {
        _appDBContext = appDBContext;
    }

    public async Task AddFaculty(FacultyDTO newFacultyData)
    {
        var newFaculty = new Faculty()
        {
            Name = newFacultyData.Name
        };

        try
        {
            await _appDBContext.Faculties.AddAsync(newFaculty);
            await _appDBContext.SaveChangesAsync();
        }
        catch (System.Exception)
        {
            throw;
        }
    }
}