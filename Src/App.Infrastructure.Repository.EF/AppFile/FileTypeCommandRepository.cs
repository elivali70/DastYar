using App.Domain.Core.AppFile.Dtos;
using App.Domain.Core.AppFile.Entities;
using App.Infrastructure.DataBase.SQL.DbContext;
using Microsoft.EntityFrameworkCore;
using System.IO.Compression;

namespace App.Domain.Core.AppFile.Comntracts.Repositories;

public class FileTypeCommandRepository:IFileTypeCommandRepository
{
    private readonly AppDbContext _appDbContext;

    public FileTypeCommandRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<int> Add(FileTypeDto fileTypeDto, CancellationToken cancellationToken)
    {
        var fileType = new FileType();
        fileType.Name = fileTypeDto.Name;
        fileType.Extention = fileTypeDto.Extention;
        fileType.CreatedAt = DateTime.Now;
        fileType.IsDeleted = false;

        await _appDbContext.AddAsync(fileType);
        await _appDbContext.SaveChangesAsync(cancellationToken);
        return fileType.Id;
    }

    public async Task Update(FileTypeDto fileTypeDto, CancellationToken cancellationToken)
    {
        var fileType = await _appDbContext.FileTypes.SingleOrDefaultAsync(x => x.Id == fileTypeDto.Id);
        if(fileType == null)
        {
            throw new Exception($"fileType with Id={fileTypeDto.Id} dosnt exist");
        }
        else
        {

        fileType.Id = fileTypeDto.Id;
        fileType.Name = fileTypeDto.Name;
        fileType.Extention = fileTypeDto.Extention;

        await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task Delete(int id, CancellationToken cancellationToken)
    {
        var fileType = await _appDbContext.FileTypes.SingleOrDefaultAsync(x => x.Id == id);
        if (fileType != null)
        {
            fileType.IsDeleted = true;
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
        else
        {
            throw new Exception($"fileType with Id={id} dosnt exist");
        }
    }
}

   
  