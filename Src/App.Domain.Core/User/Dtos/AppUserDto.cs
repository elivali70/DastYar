using App.Domain.Core.User.Enums;

namespace App.Domain.Core.User.Dtos;

public class AppUserDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string UserName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public bool IsActive { get; set; }
    public int PictureFileId { get; set; }
    public string Address { get; set; }
    public DateTime SubmitDate { get; set; }
    public RoleEnum Role { get; set; }

}
