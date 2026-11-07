using App.Domain.Core.AppFile.Comntracts.Repositories;
using App.Domain.Core.AppFile.Dtos;
using App.Domain.Core.Category.Dtos;
using App.Domain.Core.Category.Entities;
using App.Infrastructure.DataBase.SQL.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Core.Category.Contracts.Repositories
{
    public class CategoryQueryRepository:ICategoryQueryRepository
    {

        private readonly AppDbContext _appDbContext;

        public CategoryQueryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async  Task<List<CategoryDto>> GetAll(CancellationToken cancellationToken)
        {
            var categories = await _appDbContext.Categories.Where(x => !x.IsDeleted).Select(a => new CategoryDto()
            {
                Id = a.Id,
                Name = a.Name,
               DisplayOrder = a.DisplayOrder,
               ParentId = a.ParentId,
              CreatedAt = a.CreatedAt,

            }).ToListAsync(cancellationToken);
            return categories;
        }

       public async Task<CategoryDto?> Get(int id, CancellationToken cancellationToken)
        {
            var category = await _appDbContext.Categories.Where(x => !x.IsDeleted && x.Id == id).Select(a => new CategoryDto()
            {
                Id = a.Id,
                Name = a.Name,
                DisplayOrder = a.DisplayOrder,
                ParentId = a.ParentId,
                CreatedAt = a.CreatedAt,

            }).SingleOrDefaultAsync(cancellationToken);
            return category;
        }

        public async Task<CategoryDto?> Get(string name, CancellationToken cancellationToken)
        {
            var category = await _appDbContext.Categories.Where(x => !x.IsDeleted && x.Name == name).Select(a => new CategoryDto()
            {
                Id = a.Id,
                Name = a.Name,
                DisplayOrder = a.DisplayOrder,
                ParentId = a.ParentId,
                CreatedAt = a.CreatedAt,

            }).SingleOrDefaultAsync(cancellationToken);
            return category;
        }
    }
}

