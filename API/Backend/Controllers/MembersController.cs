using Backend.DAL;
using Backend.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MembersController(AppDataContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<AppUser>>> GetUsers()
    {
        return await context.Users.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AppUser>> GetUser(Guid id)
    {
        var user = await context.Users.FindAsync(id);

        return user == null ? NotFound() : user;
    }
}
