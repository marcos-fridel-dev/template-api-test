using Domain.Interfaces.Common;
using Infrastructure.Persistence.Enums.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Interfaces.Context
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class, IUnique
    {
        DbSet<TEntity> Entity { get; }
        IQueryable<TEntity> QueryInclude { get; }
        IQueryable<TEntity> QueryOrderBy { get; }
        IQueryable<TEntity> QueryIncludeOrderBy { get; }
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? OrdenBy { get; }

        TEntity Add(TEntity entity);
        IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entity);
        TEntity Delete(Guid id);
        bool Exists(Expression<Func<TEntity, bool>>? filter = null);
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>>? filter = null);
        bool ExistIdAndNotDeleted(Guid id);
        Task<bool> ExistIdAndNotDeletedAsync(Guid id);
        IEnumerable<TEntity> GetAll(int pageNumber = 1, int pageSize = 10000, IsDeleted isDeleted = IsDeleted.OnlyNotDeleted);
        IEnumerable<TEntity> GetAllInclude(int pageNumber = 1, int pageSize = 10000, IsDeleted isDeleted = IsDeleted.OnlyNotDeleted);
        Task<IEnumerable<TEntity>> GetAllAsync(int pageNumber = 1, int pageSize = 10000, IsDeleted isDeleted = IsDeleted.OnlyNotDeleted);
        Task<IEnumerable<TEntity>> GetAllIncludeAsync(int pageNumber = 1, int pageSize = 10000, IsDeleted isDeleted = IsDeleted.OnlyNotDeleted);
        TEntity? GetById(Guid id, IsDeleted isDeleted = IsDeleted.OnlyNotDeleted);
        TEntity? GetByIdInclude(Guid id, IsDeleted isDeleted = IsDeleted.OnlyNotDeleted);
        Task<TEntity?> GetByIdAsync(Guid id, IsDeleted isDeleted = IsDeleted.OnlyNotDeleted);
        Task<TEntity?> GetByIdIncludeAsync(Guid id, IsDeleted isDeleted = IsDeleted.OnlyNotDeleted);
        TEntity? GetByIdAndNotDeleted(Guid id);
        TEntity? GetByIdAndNotDeletedInclude(Guid id);
        Task<TEntity?> GetByIdAndNotDeletedAsync(Guid id);
        Task<TEntity?> GetByIdAndNotDeletedIncludeAsync(Guid id);
        TEntity Update(Guid id, TEntity entity);

        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>>? filter = null);
    }
}