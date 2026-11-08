using App.Domain.Core.AppFile.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.AppServices.AppFile
{
    public class AppFileAppService : IAppFileAppService
    {
        private readonly IAppFileService _appFileService;

        public AppFileAppService(IAppFileService appFileService)
        {
           _appFileService = appFileService;
        }
        public async Task<int> Add(AppFileDto appFileDto, CancellationToken cancellationToken)
        {
           return await _appFileService.Add(appFileDto, cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _appFileService.Delete(id, cancellationToken);
        }

        public async Task<List<AppFileAdminDto>> GetAllFileForAdmin( CancellationToken cancellationToken)
        {
            return await _appFileService.GetAllFileForAdmin( cancellationToken);
        }

        public async Task<AppFileAdminDto?> GetFileForAdmin(string? keyWord, CancellationToken cancellationToken)
        {
            return await _appFileService.GetFileForAdmin( keyWord, cancellationToken);
        }

        public async Task<AppFileAdminDto?> GetFileForAdmin(int id, CancellationToken cancellationToken)
        {
            return await _appFileService.GetFileForAdmin( id, cancellationToken);
        }
    }
}
