using App.Domain.Core.AppFile.Comntracts.Repositories;
using App.Domain.Core.AppFile.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Services.AppFile
{
    public class AppFileService : IAppFileService
    {
        private readonly IAppFileCommandRepository _appFileCommandRepository;
        private readonly IAppFileQueryRepository _appFileQueryRepository;

        public AppFileService(IAppFileCommandRepository appFileCommandRepository,
            IAppFileQueryRepository appFileQueryRepository)
        {
            _appFileCommandRepository = appFileCommandRepository;
            _appFileQueryRepository = appFileQueryRepository;
        }
        public async Task<int> Add(AppFileDto appFileDto, CancellationToken cancellationToken)
        {
            return await _appFileCommandRepository.Add(appFileDto, cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _appFileCommandRepository.Delete(id, cancellationToken);
        }

        public async Task<List<AppFileAdminDto>> GetAllFileForAdmin(CancellationToken cancellationToken)
        {
            return await _appFileQueryRepository.GetAllForAdmin(cancellationToken);
        }

        public async Task<AppFileAdminDto?> GetFileForAdmin(string? keyWord, CancellationToken cancellationToken)
        {
            return await _appFileQueryRepository.GetForAdmin(keyWord, cancellationToken);
        }

        public async Task<AppFileAdminDto?> GetFileForAdmin(int id, CancellationToken cancellationToken)
        {
            return await _appFileQueryRepository.GetForAdmin(id, cancellationToken);
        }
    }
}
