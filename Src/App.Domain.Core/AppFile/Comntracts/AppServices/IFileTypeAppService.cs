using App.Domain.Core.AppFile.Dtos;

public interface IFileTypeAppService
{
    Task<int> Add(FileTypeDto fileTypeDto,CancellationToken cancellationToken);

}