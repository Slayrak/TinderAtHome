using Backend.DAL.Models;

namespace Backend.Business.Services.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser user);

}
