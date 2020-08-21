using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces
{
    internal interface IRepository<T>
        where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);

        Task<T> GetByIdWithChildrenAsync(int id, string childrenName);

        Task<List<T>> ListAllAsync();

        Task AddAsync(T entity);

        Task UpdateAsync(T oldEntity, T newEntity);

        Task UpdateFieldAsync(T entity, string fieldName);

        Task DeleteAsync(T entity);
    }
}