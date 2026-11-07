namespace App.Domain.Core.User.Entities;

public class Expert
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public AppUser User { get; set; }
    public List<Core.Service.Entities.Service> Services { get; set; }
}
