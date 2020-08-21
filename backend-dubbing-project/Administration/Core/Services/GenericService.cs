using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Services
{
    internal abstract class GenericService<T>
        where T : BaseEntity
    {
        protected readonly IRepository<T> Repository;

        protected GenericService(IRepository<T> repository)
        {
            Repository = repository;
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await Repository.ListAllAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await Repository.GetByIdAsync(id);
        }

        public virtual async Task CreateAsync(T entity)
        {
            entity.Id = default(int);

            await Repository.AddAsync(entity);
        }

        public virtual async Task UpdateAsync(int id, T newEntity)
        {
            var oldEntity = await Repository.GetByIdAsync(id);

            if (oldEntity == null)
                throw new Exception($"{typeof(T)} entity with ID: {id} doesn't exist.");

            await Repository.UpdateAsync(oldEntity, newEntity);
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await Repository.GetByIdAsync(id);

            if (entity == null)
                return;

            await Repository.DeleteAsync(entity);
        }
    }
}