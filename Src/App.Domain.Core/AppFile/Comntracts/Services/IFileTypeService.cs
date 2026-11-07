using App.Domain.Core.AppFile.Dtos;

public interface IFileTypeService
{
    Task<int> Add(FileTypeDto fileTypeDto, CancellationToken cancellationToken);
}
