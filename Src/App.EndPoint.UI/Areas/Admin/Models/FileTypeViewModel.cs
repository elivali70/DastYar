using App.Domain.Core.AppFile.Entities;

public class FileTypeViewModel
{
    public IFormFile File { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }

}
