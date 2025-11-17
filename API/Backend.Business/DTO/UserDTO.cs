using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Business.DTO;
public class UserDTO
{
    public required string Id { get; set; }
    public required string DisplayName { get; set; }
    public required string Email { get; set; }
    public required string TokenString { get; set; }
    public string? ImageURL { get; set; }
}
