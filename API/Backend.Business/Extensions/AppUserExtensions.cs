using Backend.Business.DTO;
using Backend.DAL.Models;
using Backend.Business.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Business.Extensions;
public static class AppUserExtensions
{
    public static UserDTO ToDTO(this AppUser user, ITokenService tokenService)
    {
        return new UserDTO
        {
            Id = user.Id.ToString(),
            DisplayName = user.DisplayName,
            Email = user.Email,
            TokenString = tokenService.CreateToken(user)
        };
    }
}
