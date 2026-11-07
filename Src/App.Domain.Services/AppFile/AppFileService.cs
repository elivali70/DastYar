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

        public AppFileService(IAppFileCommandRepository appFileCommandRepository)
        {
            _appFileCommandRepository = appFileCommandRepository;
        }
        public async Task<int> Add(AppFileDto appFileDto, CancellationToken cancellationToken)
        {
            return await _appFileCommandRepository.Add(appFileDto, cancellationToken);
        }
    }
}
