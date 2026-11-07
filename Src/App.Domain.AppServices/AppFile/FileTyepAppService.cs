using App.Domain.Core.AppFile.Dtos;

public class FileTyepAppService : IFileTypeAppService
{
    private readonly IFileTypeService _fileTypeService;

    public FileTyepAppService(IFileTypeService fileTypeService)
    {
        _fileTypeService = fileTypeService;
    }
    public async Task<int> Add(FileTypeDto fileTypeDto, CancellationToken cancellationToken)
    {
        return await _fileTypeService.Add(fileTypeDto, cancellationToken);
    }
}