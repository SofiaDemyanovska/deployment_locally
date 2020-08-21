using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Infrastructure.Database
{
    internal class Repository<T> : IRepository<T>
        where T : BaseEntity
    {
        private readonly DubbingContext _dbContext;

        public Repository(DubbingContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext
                .Set<T>()
                .FindAsync(id);
        }

        public async Task<T> GetByIdWithChildrenAsync(int id, string childrenName)
        {
            return await _dbContext
                .Set<T>()
                .Include(childrenName)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<T>> ListAllAsync()
        {
            return await _dbContext
                .Set<T>()
                .ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            _dbContext
                .Set<T>()
                .Add(entity);

            await _dbContext
                .SaveChangesAsync();
        }

        public async Task UpdateAsync(T oldEntity, T newEntity)
        {
            _dbContext
                .Entry(oldEntity)
                .CurrentValues
                .SetValues(newEntity);

            await _dbContext
                .SaveChangesAsync();
        }

        public async Task UpdateFieldAsync(T entity, string fieldName)
        {
            _dbContext
                .Entry(entity)
                .Property(fieldName)
                .IsModified = true;

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext
                .Set<T>()
                .Remove(entity);

            await _dbContext
                .SaveChangesAsync();
        }
    }
}