using App.Domain.Core.AppFile.Comntracts.Repositories;
using App.Domain.Core.AppFile.Dtos;

public class FileTypeService : IFileTypeService
{
    private readonly IFileTypeCommandRepository _fileTypeCommandRepository;
    private readonly IFileTypeQueryRepository _fileTypeQueryRepository;

    public FileTypeService(IFileTypeCommandRepository fileTypeCommandRepository,
        IFileTypeQueryRepository fileTypeQueryRepository)
    {
        _fileTypeCommandRepository = fileTypeCommandRepository;
        _fileTypeQueryRepository = fileTypeQueryRepository;
    }
    public async Task<int> Add(FileTypeDto fileTypeDto, CancellationToken cancellationToken)
    {
        return await _fileTypeCommandRepository.Add(fileTypeDto, cancellationToken);
    }

    public async Task<int?> Get(FileTypeDto fileTypeDto, CancellationToken cancellationToken)
    {
        var fileType = await _fileTypeQueryRepository.Get(fileTypeDto.Name, cancellationToken);
        if (fileType == null)
        {
            return null;
        }
        return fileType.Id;
    }
}