using App.Domain.Core.AppFile.Dtos;
using App.Domain.Core.AppFile.Entities;
using App.Domain.Core.Category.Dtos;
using App.Domain.Core.Category.Entities;
using App.Infrastructure.DataBase.SQL.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Core.Category.Contracts.Repositories
{
    public class CategoryCommandRepository : ICategoryCommandRepository
    {

        private readonly AppDbContext _appDbContext;

        public CategoryCommandRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        public async Task<int> Add(CategoryDto categoryDto, CancellationToken cancellationToken)
        {
            var category = new Core.Category.Entities.Category();
            category.Name=categoryDto.Name;
            category.DisplayOrder = categoryDto.DisplayOrder;
            category.ParentId= categoryDto.ParentId;
            category.CreatedAt=DateTime.Now;

            await _appDbContext.AddAsync(category);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return category.Id;
        }

        public async Task Update(CategoryDto categoryDto, CancellationToken cancellationToken)
        {
           var category= await _appDbContext.Categories.SingleOrDefaultAsync(x=>x.Id==categoryDto.Id);
            if(category == null)
            {
                throw new Exception($" There is no Category with Id={categoryDto.Id}");
            }
            else
            {

            category.Name = categoryDto.Name;
            category.DisplayOrder = categoryDto.DisplayOrder;
            category.ParentId = categoryDto.ParentId;

            await _appDbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
           var category= await _appDbContext.Categories.SingleOrDefaultAsync(c => c.Id == id);
            if (category != null)
            {
                category.IsDeleted = true;
                _appDbContext.SaveChangesAsync(cancellationToken);
            }
            else
            {
                throw new Exception($" There is no Category with Id={id}");
            }
        }
    }
}
