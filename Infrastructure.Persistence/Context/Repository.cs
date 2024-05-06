using Domain.Interfaces.Common;
using Infrastructure.Persistence.Enums.Context;
using Infrastructure.Persistence.Interfaces.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Context
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IUnique
    {
        private bool disposedValue;

        protected readonly AppDbContext _context;
        protected DbSet<TEntity> _dbSet;

        public DbSet<TEntity> Entity => this._dbSet;
        //public IQueryable<TEntity> Query => this._dbSet.Where(x => !x.IsDeleted);

        public abstract IQueryable<TEntity> QueryInclude { get; }
        public abstract Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> OrdenBy { get; }
        public IQueryable<TEntity> QueryOrderBy => this.OrdenBy(this.Entity);
        public IQueryable<TEntity> QueryIncludeOrderBy => this.OrdenBy(this.QueryInclude);

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        #region DISPOSE
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: eliminar el estado administrado (objetos administrados)
                }

                // TODO: liberar los recursos no administrados (objetos no administrados) y reemplazar el finalizador
                // TODO: establecer los campos grandes como NULL
                disposedValue = true;
            }
        }

        // // TODO: reemplazar el finalizador solo si "Dispose(bool disposing)" tiene código para liberar los recursos no administrados
        // ~Repository()
        // {
        //     // No cambie este código. Coloque el código de limpieza en el método "Dispose(bool disposing)".
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // No cambie este código. Coloque el código de limpieza en el método "Dispose(bool disposing)".
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion

        public TEntity Add(TEntity entity)
        {
            _dbSet.Add(entity);
            return entity;
        }

        public IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entity)
        {
            _dbSet.AddRange(entity);
            return entity;
        }

        public TEntity Delete(Guid id)
        {
            TEntity? entity = GetById(id);
            return Delete(entity);
        }

        public TEntity Delete(TEntity? entity)
        {
            _dbSet.Remove(entity);
            return entity;
        }

        public TEntity Update(Guid id, TEntity entity)
        {
            entity.Id = id;
            _context.Update(entity);

            return entity;
        }

        public async Task<TEntity> DeleteAsync(Guid id)
        {
            TEntity? entity = await this.Entity
                .FirstOrDefaultAsync(x => x.Id == id);
            return Delete(entity);
        }

        public bool Exists(Expression<Func<TEntity, bool>>? filter = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query
                    .Where(filter);
            }

            return query
                .AsNoTracking()
                .Any();
        }

        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>>? filter = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query
                    .Where(filter);
            }

            return await query
                .AsNoTracking()
                .AnyAsync();
        }

        public bool ExistIdAndNotDeleted(Guid id)
        {
            return _dbSet
                .AsNoTracking()
                .Any(x => x.Id == id && !x.IsDeleted);
        }

        public async Task<bool> ExistIdAndNotDeletedAsync(Guid id)
        {
            return await _dbSet
                .AsNoTracking()
                .AnyAsync(x => x.Id == id && !x.IsDeleted);
        }

        #region GETALL

        private IEnumerable<TEntity> GetAllWithQuery(IQueryable<TEntity> query, int pageNumber = 1, int pageSize = 10000, IsDeleted isDeleted = IsDeleted.OnlyNotDeleted)
        {
            if (isDeleted != IsDeleted.All)
            {
                query = query
                    .Where(x => x.IsDeleted == (isDeleted == IsDeleted.OnlyDeleted));
            }

            query = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return query
                .AsNoTracking()
                .ToList();
        }

        public IEnumerable<TEntity> GetAll(int pageNumber = 1, int pageSize = 10000, IsDeleted isDeleted = IsDeleted.OnlyNotDeleted) =>
            GetAllWithQuery(this.QueryOrderBy, pageNumber, pageSize, isDeleted);

        public IEnumerable<TEntity> GetAllInclude(int pageNumber = 1, int pageSize = 10000, IsDeleted isDeleted = IsDeleted.OnlyNotDeleted) =>
            GetAllWithQuery(this.QueryIncludeOrderBy, pageNumber, pageSize, isDeleted);

        private async Task<IEnumerable<TEntity>> GetAllIncludeWithQueryAsync(IQueryable<TEntity> query, int? pageNumber = 1, int? pageSize = 10000, IsDeleted? isDeleted = IsDeleted.OnlyNotDeleted)
        {
            if (isDeleted != IsDeleted.All)
            {
                query = query
                    .Where(x => x.IsDeleted == (isDeleted == IsDeleted.OnlyDeleted));
            }

            query = query
                .Skip(((pageNumber ?? 1) - 1) * (pageSize ?? 10000))
                .Take(pageSize ?? 10000);


            return await query
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(int pageNumber = 1, int pageSize = 10000, IsDeleted isDeleted = IsDeleted.OnlyNotDeleted) =>
            await GetAllIncludeWithQueryAsync(this.Entity, pageNumber, pageSize, isDeleted);

        public async Task<IEnumerable<TEntity>> GetAllIncludeAsync(int pageNumber = 1, int pageSize = 10000, IsDeleted isDeleted = IsDeleted.OnlyNotDeleted) =>
            await GetAllIncludeWithQueryAsync(this.QueryIncludeOrderBy, pageNumber, pageSize, isDeleted);
        #endregion

        #region GETBYID
        public TEntity? GetByIdWithQuery(IQueryable<TEntity> query, Guid id, IsDeleted isDeleted = IsDeleted.OnlyNotDeleted)
        {
            if (isDeleted != IsDeleted.All)
            {
                query = query
                    .Where(x => x.IsDeleted == (isDeleted == IsDeleted.OnlyDeleted));
            }

            return query
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == id);
        }

        public async Task<TEntity?> GetByIdWithQueryAsync(IQueryable<TEntity> query, Guid id, IsDeleted isDeleted = IsDeleted.OnlyNotDeleted)
        {
            if (isDeleted != IsDeleted.All)
            {
                query = query
                    .Where(x => x.IsDeleted == (isDeleted == IsDeleted.OnlyDeleted));
            }


            return await query
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public TEntity? GetById(Guid id, IsDeleted isDeleted = IsDeleted.OnlyNotDeleted) =>
            GetByIdWithQuery(this.Entity, id, isDeleted);

        public TEntity? GetByIdInclude(Guid id, IsDeleted isDeleted = IsDeleted.OnlyNotDeleted) =>
            GetByIdWithQuery(this.QueryInclude, id, isDeleted);

        public async Task<TEntity?> GetByIdAsync(Guid id, IsDeleted isDeleted = IsDeleted.OnlyNotDeleted) =>
            await GetByIdWithQueryAsync(this.Entity, id, isDeleted);

        public async Task<TEntity?> GetByIdIncludeAsync(Guid id, IsDeleted isDeleted = IsDeleted.OnlyNotDeleted) =>
            await GetByIdWithQueryAsync(this.QueryInclude, id, isDeleted);

        public TEntity? GetByIdAndNotDeleted(Guid id) =>
            GetByIdWithQuery(this.Entity, id, IsDeleted.OnlyNotDeleted);
        public TEntity? GetByIdAndNotDeletedInclude(Guid id) =>
            GetByIdWithQuery(this.QueryInclude, id, IsDeleted.OnlyNotDeleted);

        public async Task<TEntity?> GetByIdAndNotDeletedAsync(Guid id) =>
            await GetByIdWithQueryAsync(this.Entity, id, IsDeleted.OnlyNotDeleted);

        public async Task<TEntity?> GetByIdAndNotDeletedIncludeAsync(Guid id) =>
            await GetByIdWithQueryAsync(this.QueryInclude, id, IsDeleted.OnlyNotDeleted);
        #endregion

        public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>>? filter = null) =>
            await this.Entity.FirstOrDefaultAsync(filter);

        //public IRepository<TEntity> AsNoTracking()
        //{
        //    _asNoTracking = true;
        //    return this;
        //}




        //public TEntity? FirstOrDefault(
        //    Expression<Func<TEntity, bool>> filter = null,
        //    isDeleted isDeleted = isDeleted.OnlyNotDeleted)
        //{
        //    if (_asNoTracking)
        //    {
        //        _asNoTracking = false;
        //        return _dbSet
        //            .AsNoTracking()
        //            .FirstOrDefault(filter);
        //    }

        //    return _dbSet
        //        .FirstOrDefault(filter);
        //}

        //public IEnumerable<TEntity> Find(
        //    Expression<Func<TEntity, bool>>? filter = null,
        //    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        //    isDeleted isDeleted = isDeleted.OnlyNotDeleted)
        //{
        //    IQueryable<TEntity> query = _dbSet;

        //    if (filter != null)
        //    {
        //        query = query
        //            .Where(filter);
        //    }

        //    if (isDeleted != isDeleted.All)
        //    {
        //        query = query
        //            .Where(x => x.IsDeleted == (isDeleted == isDeleted.OnlyDeleted));
        //    }


        //    if (orderBy != null)
        //    {
        //        if (_asNoTracking)
        //        {
        //            _asNoTracking = false;
        //            return orderBy(query)
        //                .AsNoTracking()
        //                .ToList();
        //        }

        //        return orderBy(query)
        //            .ToList();
        //    }

        //    if (_asNoTracking)
        //    {
        //        _asNoTracking = false;
        //        return query
        //            .AsNoTracking()
        //            .ToList();
        //    }

        //    return query
        //        .ToList();
        //}

        //public async Task<TEntity?> FirstOrDefaultAsync(
        //    Expression<Func<TEntity, bool>> filter = null,
        //    isDeleted isDeleted = isDeleted.OnlyNotDeleted)
        //{
        //    if (_asNoTracking)
        //    {
        //        _asNoTracking = false;
        //        return await _dbSet
        //            .AsNoTracking()
        //            .FirstOrDefaultAsync(filter);
        //    }

        //    return await _dbSet
        //        .FirstOrDefaultAsync(filter);
        //}

        //public async Task<IEnumerable<TEntity>> GetAllAsync(isDeleted isDeleted = isDeleted.OnlyNotDeleted)
        //{
        //    return await FindAsync(null, null, isDeleted);
        //}

        //public async Task<TEntity?> GetByIdAsync(Guid id)
        //{
        //    if (_asNoTracking)
        //    {
        //        _asNoTracking = false;
        //        return await _dbSet
        //            .AsNoTracking()
        //            .FirstOrDefaultAsync(x => x.Id == id);
        //    }

        //    return await _dbSet
        //        .FirstOrDefaultAsync(x => x.Id == id);
        //}

        //public async Task<IEnumerable<TEntity>> FindAsync(
        //    Expression<Func<TEntity, bool>>? filter = null,
        //    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        //    isDeleted isDeleted = isDeleted.OnlyNotDeleted)
        //{
        //    IQueryable<TEntity> query = _dbSet;

        //    if (filter != null)
        //    {
        //        query = query
        //            .Where(filter);
        //    }

        //    if (isDeleted != isDeleted.All)
        //    {
        //        query = query
        //            .Where(x => x.IsDeleted == (isDeleted == isDeleted.OnlyDeleted));
        //    }


        //    if (orderBy != null)
        //    {
        //        if (_asNoTracking)
        //        {
        //            _asNoTracking = false;
        //            return await orderBy(query)
        //                .AsNoTracking()
        //                .ToListAsync();
        //        }
        //        return await orderBy(query)
        //            .ToListAsync();
        //    }

        //    if (_asNoTracking)
        //    {
        //        _asNoTracking = false;
        //        return await query
        //            .AsNoTracking()
        //            .ToListAsync();
        //    }

        //    return await query
        //        .ToListAsync();
        //}

        //public async Task<TEntity> CreateAsync(TEntity entity)
        //{
        //    await _dbSet.AddAsync(entity);
        //    return entity;
        //}

        //public async Task<IEnumerable<TEntity>> CreateRangeAsync(IEnumerable<TEntity> entity)
        //{
        //    await _dbSet.AddRangeAsync(entity);
        //    return entity;
        //}

        //public bool ExistIdAndNotDeleted(Guid id)
        //{
        //    if (_asNoTracking)
        //    {
        //        _asNoTracking = false;
        //        return _dbSet
        //            .AsNoTracking()
        //            .FirstOrDefault(x => x.Id == id && !x.IsDeleted) != null;
        //    }

        //    return _dbSet
        //        .FirstOrDefault(x => x.Id == id && !x.IsDeleted) != null;
        //}

        //public async Task<bool> ExistIdAndNotDeletedAsync(Guid id)
        //{
        //    return (await _dbSet.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted)) != null;
        //}

        //public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>>? filter = null)
        //{
        //    IQueryable<TEntity> query = _dbSet;

        //    if (filter != null)
        //    {
        //        if (_asNoTracking)
        //        {
        //            _asNoTracking = false;
        //            return await query
        //                .AsNoTracking()
        //                .FirstOrDefaultAsync(filter) != null;
        //        }

        //        return await query
        //            .FirstOrDefaultAsync(filter) != null;
        //    }

        //    if (_asNoTracking)
        //    {
        //        _asNoTracking = false;
        //        return await query
        //            .AsNoTracking()
        //            .FirstOrDefaultAsync() != null;
        //    }

        //    return await query
        //        .FirstOrDefaultAsync() != null;
        //}

        //public Task<bool> ExistsAsync(Expression<Func<TEntity, bool>>? filter = null)
        //{
        //    IQueryable<TEntity> query = _dbSet;

        //    if (filter != null)
        //    {
        //        query = query
        //            .Where(filter);
        //    }

        //    return await query
        //        .AsNoTracking()
        //        .();
        //}
    }
}