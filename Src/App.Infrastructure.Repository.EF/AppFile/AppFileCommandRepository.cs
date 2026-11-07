using App.Domain.Core.AppFile.Dtos;
using App.Domain.Core.AppFile.Entities;
using App.Domain.Core.Order.Enities;
using App.Infrastructure.DataBase.SQL.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.NativeInterop;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Core.AppFile.Comntracts.Repositories
{
    public class AppFileCommandRepository : IAppFileCommandRepository
    {
        private readonly AppDbContext _appDbContext;

        public AppFileCommandRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var appFile = await _appDbContext.Files.SingleOrDefaultAsync(x => x.Id == id);
            if (appFile != null)
            {
                appFile.IsDeleted = true;
                await _appDbContext.SaveChangesAsync(cancellationToken);
            }
            else
            {
                throw new Exception($"File with Id={id} dosnt exist");
            }
        }

        public async Task Update(AppFileDto appFileDto, CancellationToken cancellationToken)
        {
            var appFile = await _appDbContext.Files.SingleOrDefaultAsync(x => x.Id == appFileDto.Id);
            if(appFile != null)
            {
            appFile.Id = appFileDto.Id;
            appFile.Name = appFileDto.Name;
            appFile.Path = appFileDto.Path;
            appFile.Description = appFileDto.Description;
            appFile.FileTypeId = appFileDto.FileTypeId;
            appFile.UserId = appFileDto.UserId;
            appFile.CreatedAt = appFileDto.CreatedAt;

            await _appDbContext.SaveChangesAsync(cancellationToken);
            }
            else
            {
                throw new Exception($"File with Id={appFileDto.Id} dosnt exist");

            }
        }

        public async Task<int> Add(AppFileDto appFileDto, CancellationToken cancellationToken)
        {
            var appFile= new Core.AppFile.Entities.AppFile();
           
            appFile.Name = appFileDto.Name;
            appFile.Path = appFileDto.Path;
            appFile.Description = appFileDto.Description;
            appFile.FileTypeId = appFileDto.FileTypeId;
            appFile.UserId = appFileDto.UserId;
            appFile.CreatedAt = DateTime.Now;
            appFile.IsDeleted = false;

            await _appDbContext.AddAsync(appFile);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return appFile.Id;
        }
    }
}
