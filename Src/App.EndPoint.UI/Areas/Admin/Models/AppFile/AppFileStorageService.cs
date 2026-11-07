
using App.EndPoint.UI.Areas.Admin.Models.AppFile;

public class AppFileStorageService : IAppFileStorageService
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public AppFileStorageService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }
    public string CreatFolder(int Id, string name)
    {
        var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "upload", name, Id.ToString());
        Directory.CreateDirectory(folderPath);
        return folderPath;
    }

    public async Task<string> SaveFile(IFormFile file, string folderPath)
    {
        
            var extention = Path.GetExtension(file.Name);
            var fileName = $"{Guid.NewGuid()}{extention}";
            var filePath = Path.Combine(folderPath, fileName);
            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);
           
        return filePath ;
    }
}