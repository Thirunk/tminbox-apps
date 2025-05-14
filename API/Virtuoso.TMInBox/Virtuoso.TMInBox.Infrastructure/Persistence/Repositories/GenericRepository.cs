using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Virtuoso.TMInBox.Core.DTOs;
using Virtuoso.TMInBox.Core.Interfaces.Repository;

namespace Virtuoso.TMInBox.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<TEntity,TKey>:IGenericRepository<TEntity,TKey> where TEntity : class  
    {
        protected readonly DbContext DbContext;

        private readonly DbSet<TEntity> _entitySet;
        public GenericRepository(DbContext dbContext)
        {
            DbContext = dbContext;
            _entitySet = DbContext.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _entitySet.ToList();
        }

        public virtual TEntity? GetById(TKey id)
        {
            return _entitySet.Find(typeof(TEntity), id);
        }
        public virtual void Add(TEntity entity)
        {
            _entitySet.Add(entity);
        }
        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            _entitySet.AddRange(entities);
            ;
        }

        public virtual void Update(TEntity entity)
        {
            _entitySet.Attach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            _entitySet.UpdateRange(entities);
        }
        public virtual void Remove(TEntity entity)
        {
            if (DbContext.Entry(entity).State == EntityState.Detached)
            {
                _entitySet.Attach(entity);
            }
            _entitySet.Remove(entity);
        }

        public virtual void Remove(TKey id)
        {
            TEntity? val = _entitySet?.Find(typeof(TEntity), id);
            if (val != null)
            {
                _entitySet?.Remove(val);
            }
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            _entitySet.RemoveRange(entities);
        }

        public virtual IEnumerable<TEntity?> Get(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> queryable = GetQueryable(filter, orderBy, includeProperties);
            return queryable.ToList();
        }

        public virtual IEnumerable<TEntity> GetAsNoTracking(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderby = null, string includeProperties = "")
        {
            IQueryable<TEntity> queryable = GetQueryable(filter, orderby, includeProperties);
            return EntityFrameworkQueryableExtensions.AsNoTracking(queryable).ToList();
        }
        private IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> queryable = _entitySet;

            if (filter != null)
            {
                queryable = queryable.Where(filter);
            }
            string[] array = includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries);
            foreach (string text in array)
            {

                queryable = EntityFrameworkQueryableExtensions.Include(queryable, text.Trim());

            }

            if (orderBy != null)
            {
                return orderBy(queryable);
            }


            return queryable;
        }

        public virtual IEnumerable<T> Get<T>(Expression<Func<TEntity, T>> select, Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string includeProperties = "") where T : class
        {
            IQueryable<TEntity> queryable = GetQueryable(filter, orderBy, includeProperties);
            return EntityFrameworkQueryableExtensions.AsNoTracking(queryable.Select(select)).ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await EntityFrameworkQueryableExtensions.ToListAsync(_entitySet, cancellationToken);
        }

        public virtual async Task<TEntity?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _entitySet.FindAsync([id], cancellationToken);
        }


        public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
        {
           await _entitySet.AddAsync(entity, cancellationToken);
        }
              
        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken))
        {
            await _entitySet.AddRangeAsync(entities, cancellationToken);
        }
       
        public virtual async Task<IEnumerable<TEntity?>> GetAsync(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string includeProperties = "", CancellationToken cancellationToken = default(CancellationToken))
        {
            IQueryable<TEntity> queryable = GetQueryable(filter, orderBy, includeProperties);
            return await EntityFrameworkQueryableExtensions.ToListAsync(queryable, cancellationToken);
        }

        public virtual async Task<IEnumerable<TEntity?>> GetAsNoTrackingAsync(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string includeProperties = "", CancellationToken cancellationToken = default(CancellationToken))
        {
            IQueryable<TEntity> queryable = GetQueryable(filter, orderBy, includeProperties);
            return await EntityFrameworkQueryableExtensions.ToListAsync(EntityFrameworkQueryableExtensions.AsNoTracking(queryable), cancellationToken);
        }

        public virtual async Task<IEnumerable<T>> GetAsync<T>(Expression<Func<TEntity, T>> select, Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string includeProperties = "", CancellationToken cancellationToken = default(CancellationToken)) where T : class
        {
            IQueryable<TEntity> queryable = GetQueryable(filter, orderBy, includeProperties);
            return await EntityFrameworkQueryableExtensions.ToListAsync(EntityFrameworkQueryableExtensions.AsNoTracking(queryable.Select(select)),cancellationToken);

        }

        public virtual async Task<PagedResult<TEntity>> GetAllPaginatedAsync( int pageNumber, int pageSize, CancellationToken cancellationToken = default(CancellationToken))
        {
            IQueryable<TEntity> query = EntityFrameworkQueryableExtensions.AsNoTracking(_entitySet).Skip((pageNumber-1)*pageSize).Take(pageSize);
            return new PagedResult<TEntity>(await EntityFrameworkQueryableExtensions.ToListAsync(query,cancellationToken), await EntityFrameworkQueryableExtensions.CountAsync(query,cancellationToken),pageNumber,pageSize);
        }
        public virtual async Task<PagedResult<TEntity>> GetPaginatedAsync(int pageNumber, int pageSize, Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string includeProperties = "", CancellationToken cancellationToken = default(CancellationToken))
        {
            IQueryable<TEntity> query = EntityFrameworkQueryableExtensions.AsNoTracking(GetQueryable(filter,orderBy,includeProperties)).Skip((pageNumber - 1) * pageSize).Take(pageSize);
            return new PagedResult<TEntity>(await EntityFrameworkQueryableExtensions.ToListAsync(query, cancellationToken), await EntityFrameworkQueryableExtensions.CountAsync(query, cancellationToken), pageNumber, pageSize);
        }

        public virtual async Task<PagedResult<T>> GetPaginatedAsync<T>(int pageNumber, int pageSize, Expression<Func<TEntity, T>> select, Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string includeProperties = "", CancellationToken cancellationToken=default(CancellationToken)) where T : class
        {
            IQueryable<T> query = EntityFrameworkQueryableExtensions.AsNoTracking(GetQueryable(filter, orderBy, includeProperties).Select(select)).Skip((pageNumber - 1) * pageSize).Take(pageSize);
            return new PagedResult<T>(await EntityFrameworkQueryableExtensions.ToListAsync(query, cancellationToken), await EntityFrameworkQueryableExtensions.CountAsync(query, cancellationToken), pageNumber, pageSize);
        }







    }
}
