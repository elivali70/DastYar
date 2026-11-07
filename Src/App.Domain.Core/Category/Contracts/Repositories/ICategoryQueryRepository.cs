using App.Domain.Core.Category.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Core.Category.Contracts.Repositories
{
    public interface ICategoryQueryRepository
    {
        Task<List<CategoryDto>> GetAll(CancellationToken cancellationToken);
        Task<CategoryDto?> Get(int id, CancellationToken cancellationToken);
        Task<CategoryDto?> Get(string name, CancellationToken cancellationToken);
    }
}
