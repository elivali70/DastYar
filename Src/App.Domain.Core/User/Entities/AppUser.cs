using App.Domain.Core.User.Enums;
using Microsoft.AspNetCore.Identity;

namespace App.Domain.Core.User.Entities;

public class AppUser : IdentityUser<int>
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public bool IsActive { get; set; }
    public int PictureFileId { get; set; }
    public string Address { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime SubmitDate { get; set; }
    public RoleEnum Role { get; set; }
}
