using App.Domain.Core.Service.Contracts.AppServices;
using App.Domain.Core.Service.Contracts.Services;
using App.Domain.Core.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.AppServices.Service
{
    public class ServiceAppService : IServiceAppService
    {
        private readonly IServiceService _serviceService;

        public ServiceAppService(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        public Task<ServiceDto?> Get(int id, CancellationToken cancellationToken)
        {
            return _serviceService.Get(id, cancellationToken);
        }

        public async Task<int> GetId(string name, CancellationToken cancellationToken)
        {
            return await _serviceService.GetId(name, cancellationToken);
        }
    }
}
