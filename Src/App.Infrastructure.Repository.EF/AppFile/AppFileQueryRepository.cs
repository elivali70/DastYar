using App.Domain.Core.AppFile.Dtos;
using App.Domain.Core.AppFile.Entities;
using App.Infrastructure.DataBase.SQL.DbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Core.AppFile.Comntracts.Repositories
{
    public class AppFileQueryRepository: IAppFileQueryRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<IdentityUser<int>> _userManager;

        public AppFileQueryRepository(AppDbContext appDbContext,
            UserManager<IdentityUser<int>> userManager)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
        }
        public async Task<List<AppFileDto>> GetAll(CancellationToken cancellationToken)
        {
            var appFiles = await _appDbContext.Files.Where(x => !x.IsDeleted).Select(a => new AppFileDto()
            {
                Id = a.Id,
                Name = a.Name,
                Path = a.Path,
                Description = a.Description,
                UserId=a.UserId,
                CreatedAt=a.CreatedAt,
                FileTypeId=a.FileTypeId,
                
            }).ToListAsync(cancellationToken);
            return appFiles;
        }

        public async Task<AppFileDto?> Get(int id, CancellationToken cancellationToken)
        {
            var appFile = await _appDbContext.Files.Where(x => x.Id == id && !x.IsDeleted).Select(a => new AppFileDto()
            {

                Id = a.Id,
                Name = a.Name,
                Path = a.Path,
                Description = a.Description,
                UserId = a.UserId,
                CreatedAt = a.CreatedAt,
                FileTypeId = a.FileTypeId,

            }).SingleOrDefaultAsync(cancellationToken);
            return appFile;

        }
        public async Task<AppFileDto?> Get(string name, CancellationToken cancellationToken)
        {
            var appFile = await _appDbContext.Files.Where(x => x.Name == name && !x.IsDeleted).Select(a => new AppFileDto()
            {

                Id = a.Id,
                Name = a.Name,
                Path = a.Path,
                Description = a.Description,
                UserId = a.UserId,
                CreatedAt = a.CreatedAt,
                FileTypeId = a.FileTypeId,

            }).SingleOrDefaultAsync(cancellationToken);
            return appFile;
        }

        public async Task<List<AppFileAdminDto>> GetAllForAdmin(CancellationToken cancellationToken)
        {
            var appFiles = await _appDbContext.Files.Where(x => !x.IsDeleted).Select( a => new AppFileAdminDto()
            {
                Id = a.Id,
                Name = a.Name,
                Path = a.Path,
                Description = a.Description,
                UserId = a.UserId,
                CreatedAt=a.CreatedAt,
                FileTypeName=a.FileType.Name,
                Extention=a.FileType.Extention,
                IsUsed= a.OrderFiles.Any() || a.ServiceFiles.Any(),
                UploaderUserName = _userManager.Users.Where(u => u.Id == a.UserId).Select(u => u.UserName).FirstOrDefault()

            }).ToListAsync(cancellationToken);
           
            return appFiles;
        }
    }
}
