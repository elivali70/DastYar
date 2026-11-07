namespace App.Domain.Core.User.Entities;

public class Admin
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public AppUser User { get; set; } = null!;


}
