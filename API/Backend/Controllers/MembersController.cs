using Backend.DAL;
using Backend.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

public class MembersController(AppDataContext context) : BaseApiController
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
