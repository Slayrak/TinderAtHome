namespace Backend.DAL.Models;
public class AppUser
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string DisplayName { get; set; }
    public required string Email { get; set; }
}
