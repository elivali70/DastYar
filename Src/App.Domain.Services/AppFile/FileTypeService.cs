using App.Domain.Core.AppFile.Comntracts.Repositories;
using App.Domain.Core.AppFile.Dtos;

public class FileTypeService : IFileTypeService
{
    private readonly IFileTypeCommandRepository _fileTypeCommandRepository;

    public FileTypeService(IFileTypeCommandRepository fileTypeCommandRepository)
    {
        _fileTypeCommandRepository = fileTypeCommandRepository;
    }
    public async Task<int> Add(FileTypeDto fileTypeDto, CancellationToken cancellationToken)
    {
        return await _fileTypeCommandRepository.Add(fileTypeDto,cancellationToken);
    }
}