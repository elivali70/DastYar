using App.Domain.Core.AppFile.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Core.AppFile.Comntracts.Repositories
{
    public interface IAppFileCommandRepository
    {
        Task Delete(int id,CancellationToken cancellationToken);
        Task Update(AppFileDto appFileDto,CancellationToken cancellationToken);
        Task<int> Add(AppFileDto appFileDto, CancellationToken cancellationToken);
    }
}
