using App.Domain.Core.AppFile.Dtos;

namespace App.Domain.Core.AppFile.Comntracts.Repositories;

public interface IFileTypeCommandRepository
{
    Task<int> Add(FileTypeDto fileTypeDto,CancellationToken cancellationToken);
    Task Update(FileTypeDto fileTypeDto,CancellationToken cancellationToken);
    Task Delete(int id, CancellationToken cancellationToken);
}
