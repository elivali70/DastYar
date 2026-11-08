using App.Domain.Core.AppFile.Dtos;

public interface IAppFileAppService
{
    Task<int> Add(AppFileDto appFileDto, CancellationToken cancellationToken);
    Task<List<AppFileAdminDto>> GetAllFileForAdmin(CancellationToken cancellationToken);
    Task<AppFileAdminDto?> GetFileForAdmin(string? keyWord, CancellationToken cancellationToken);
    Task<AppFileAdminDto?> GetFileForAdmin(int id, CancellationToken cancellationToken);
    Task Delete(int id, CancellationToken cancellationToken);

}
