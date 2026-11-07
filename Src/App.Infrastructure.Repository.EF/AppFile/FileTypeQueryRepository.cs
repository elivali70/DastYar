using App.Domain.Core.AppFile.Dtos;
using App.Infrastructure.DataBase.SQL.DbContext;
using Microsoft.EntityFrameworkCore;

namespace App.Domain.Core.AppFile.Comntracts.Repositories;

public class FileTypeQueryRepository:IFileTypeQueryRepository
{
    private readonly AppDbContext _appDbContext;

    public FileTypeQueryRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<FileTypeDto>> GetAll(CancellationToken cancellationToken)
    {
        var fileTypes = await _appDbContext.FileTypes.Where(x => !x.IsDeleted).Select(s => new FileTypeDto()
        {
            Id=s.Id,
            Name=s.Name,
            Extention=s.Extention
        }).ToListAsync(cancellationToken);
        return fileTypes;
    }

    public async Task<FileTypeDto?> Get(int id, CancellationToken cancellationToken)
    {
        var fileType = await _appDbContext.FileTypes.Where(x => x.Id == id && !x.IsDeleted).Select(s => new FileTypeDto()
        {
            Id = s.Id,
            Name = s.Name,
            Extention = s.Extention
        }).SingleOrDefaultAsync(cancellationToken);
        return fileType;
    }

    public async Task<FileTypeDto?> Get(string name, CancellationToken cancellationToken)
    {
        var fileType = await _appDbContext.FileTypes.Where(x => x.Name == name && !x.IsDeleted).Select(s => new FileTypeDto()
        {
            Id = s.Id,
            Name = s.Name,
            Extention = s.Extention
        }).SingleOrDefaultAsync(cancellationToken);
        return fileType;
    }
}

  