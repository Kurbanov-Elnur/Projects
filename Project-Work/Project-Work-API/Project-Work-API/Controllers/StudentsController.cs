
using Microsoft.AspNetCore.Mvc;
using Project_Work_API.Data.Models.DBModels;
using Project_Work_API.Data.Models.DTO;

namespace Project_Work_API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class StudentsController : ControllerBase
{


    [HttpPost("AddStudent")]
    public async Task<IActionResult> AddStudent([FromBody] StudentDTO Student)
    {
        return Ok("Hello");
    }
}