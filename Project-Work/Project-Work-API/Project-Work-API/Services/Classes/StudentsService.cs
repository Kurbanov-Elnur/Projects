
using Project_Work_API.Data.Contexts;
using Project_Work_API.Services.Interfaces;

namespace Project_Work_API.Services.Classes;

public class StudentsService : IStudentsService
{
    private readonly AppDBContext _appDBContext;

    public StudentsService(AppDBContext appDBContext)
    {
        _appDBContext = appDBContext;
    }

    // public async Task<bool> AddStudentAsync()
    // {

    // }
}