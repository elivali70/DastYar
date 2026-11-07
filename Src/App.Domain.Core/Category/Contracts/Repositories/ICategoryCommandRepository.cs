using App.Domain.Core.Category.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Core.Category.Contracts.Repositories
{
    public interface ICategoryCommandRepository
    {
        Task<int> Add(CategoryDto categoryDto, CancellationToken cancellationToken);
        Task Update(CategoryDto categoryDto, CancellationToken cancellationToken);
        Task Delete(int id,CancellationToken cancellationToken);
    }
}
