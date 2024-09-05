
using Microsoft.AspNetCore.Mvc;
using Project_Work_API.Data.Models.DTO;
using Project_Work_API.Services.Interfaces;

namespace Project_Work_API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class FacultyController : ControllerBase
{
    private readonly IFacultyService _facultyService;

    public FacultyController(IFacultyService facultyService)
    {
        _facultyService = facultyService;
    }

    [HttpPost("AddFaculty")]
    public async Task<IActionResult> AddFaculty([FromBody] FacultyDTO Faculty)
    {
        try
        {
            await _facultyService.AddFaculty(Faculty);

            return Ok("Faculty has been successfully added");
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}