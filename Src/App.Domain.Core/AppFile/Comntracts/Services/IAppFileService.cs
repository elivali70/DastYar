using App.Domain.Core.AppFile.Dtos;
using System;
using System.Collections.Generic;
using System.Text;


public interface IAppFileService
{
    Task<int> Add(AppFileDto appFileDto, CancellationToken cancellationToken);
    Task<List<AppFileAdminDto>> GetAllFileForAdmin(CancellationToken cancellationToken);
    Task<AppFileAdminDto?> GetFileForAdmin(string? keyWord,CancellationToken cancellationToken);
    Task<AppFileAdminDto?> GetFileForAdmin(int id, CancellationToken cancellationToken);
    Task Delete(int id, CancellationToken cancellationToken);


}
