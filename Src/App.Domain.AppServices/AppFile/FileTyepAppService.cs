using App.Domain.Core.AppFile.Dtos;
using System.ComponentModel.Design;

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

    public async Task<int> GetOrAdd(FileTypeDto fileTypeDto, CancellationToken cancellationToken)
    {
        var filTypeId = await _fileTypeService.Get(fileTypeDto, cancellationToken);
        if (filTypeId == null)
        {
            var id = await _fileTypeService.Add(fileTypeDto, cancellationToken);
            return id;
        }
        else

            return (int)filTypeId;

    }
}