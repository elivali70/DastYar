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
    }
}
