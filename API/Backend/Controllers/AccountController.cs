using Backend.Business.DTO;
using Backend.Business.Extensions;
using Backend.Business.Services.Interfaces;
using Backend.DAL;
using Backend.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Backend.Controllers;

public class AccountController(AppDataContext context, ITokenService tokenService) : BaseApiController
{
    [HttpPost("register")]
    public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
    {
        if (await EmailExists(registerDTO.Email))
        {
            return BadRequest("Email was taken");
        }

        using var hmac = new HMACSHA512();

        var newUser = new AppUser
        {
            Email = registerDTO.Email,
            DisplayName = registerDTO.DisplayName,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
            PasswordSalt = hmac.Key
        };

        context.Add(newUser);
        await context.SaveChangesAsync();

        return newUser.ToDTO(tokenService);
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
    {
        var user = await context.Users.SingleOrDefaultAsync(x => x.Email == loginDTO.Email);

        if (user == null) 
        {
            return Unauthorized("Invalid email");
        }

        using var hmac = new HMACSHA512(user.PasswordSalt);

        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));

        for (int i = 0; i < computedHash.Length; i++) 
        {
            if (computedHash[i] != user.PasswordHash[i])
            {
                return Unauthorized("Password is incorrect");
            }
        }

        return user.ToDTO(tokenService);
    }

    private async Task<bool> EmailExists(string email)
    {
        return await context.Users.AnyAsync(u => u.Email == email);
    }
}
