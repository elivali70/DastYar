using App.Domain.Core.AppFile.Dtos;

namespace App.Domain.Core.AppFile.Comntracts.Repositories;

public interface IFileTypeQueryRepository
{
    Task<List<FileTypeDto>> GetAll(CancellationToken cancellationToken);
    Task<FileTypeDto?> Get(int  id,CancellationToken cancellationToken);
    Task<FileTypeDto?> Get(string  name,CancellationToken cancellationToken);
}