using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Virtuoso.TMInBox.Core.Interfaces.Repository
{
    public interface IGenericRepository<TEntity, Tkey> where TEntity : class
    {
        TEntity? GetById(Tkey id);
        IEnumerable<TEntity?> Get(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? oderby = null, string includeProperties = "");
        IEnumerable<T> Get<T>(Expression<Func<TEntity, T>> select, Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? oderby = null, string includeProperties = "") where T : class;
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);    
        void UpdateRange(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        Task<TEntity?> GetByIdAsync(Tkey id, CancellationToken cancellationToken=default(CancellationToken));
        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));
        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken));
        Task<IEnumerable<TEntity?>> GetAsync(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? oderby = null, string includeProperties = "", CancellationToken cancellationToken = default(CancellationToken));
        Task<IEnumerable<T>> GetAsync<T>(Expression<Func<TEntity, T>> select, Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? oderby = null, string includeProperties = "", CancellationToken cancellationToken = default(CancellationToken)) where T : class;




    }
}
