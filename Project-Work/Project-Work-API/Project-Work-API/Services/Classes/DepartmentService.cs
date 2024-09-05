
using Project_Work_API.Data.Contexts;
using Project_Work_API.Services.Interfaces;

namespace Project_Work_API.Services.Classes;

public class DepartmentService : IDepartmentService
{
    private readonly AppDBContext _appDBContext;
}