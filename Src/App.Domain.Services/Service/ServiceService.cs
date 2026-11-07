using App.Domain.Core.Service.Contracts.Repositories;
using App.Domain.Core.Service.Contracts.Services;
using App.Domain.Core.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Services.Service
{
    public class ServiceService:IServiceService
    {
        private readonly IServiceQueryRepository _serviceQueryRepository;

        public ServiceService(IServiceQueryRepository serviceQueryRepository)
        {
            _serviceQueryRepository = serviceQueryRepository;
        }

        public async Task<ServiceDto?> Get(int id, CancellationToken cancellationToken)
        {
            return await _serviceQueryRepository.Get(id, cancellationToken);
        }

        public async Task<int> GetId(string name, CancellationToken cancellationToken)
        {
            var service = await _serviceQueryRepository.Get(name, cancellationToken);
            return service.Id;
        }
    }
}
