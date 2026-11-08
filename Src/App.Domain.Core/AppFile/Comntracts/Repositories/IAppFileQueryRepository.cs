using App.Domain.Core.AppFile.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Core.AppFile.Comntracts.Repositories
{
    public interface IAppFileQueryRepository
    {
        Task<List<AppFileDto>> GetAll(CancellationToken cancellationToken);
        Task<AppFileDto?> Get(int  id,CancellationToken cancellationToken);
        Task<AppFileDto?> Get(string name, CancellationToken cancellationToken);
        Task<List<AppFileAdminDto>> GetAllForAdmin(CancellationToken cancellationToken);
    }
}
