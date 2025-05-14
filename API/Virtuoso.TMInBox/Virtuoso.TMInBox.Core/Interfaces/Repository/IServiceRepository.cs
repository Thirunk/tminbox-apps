using System.Linq.Expressions;
using Virtuoso.TMInBox.Core.DTOs;
using Virtuoso.TMInBox.Core.Models;

namespace Virtuoso.TMInBox.Core.Interfaces.Repository
{
    public interface IServiceRepository
    {
        Task AddAsync(Service services, CancellationToken cancellationToken=default);
        Task<Service> GetByIdAsync(int id, CancellationToken cancellationToken=default);
        Task<PagedResult<Service>> GetAllPaginatedAsync(int pageNumber,int pageSize,CancellationToken cancellationToken=default);
        Task<IEnumerable<Service>> GetAsync(
            Expression<Func<Service, bool>>? filter = null,
            Func<IQueryable<Service>, IOrderedQueryable<Service>>? orderBy = null,
            string includeProperties = "", CancellationToken cancellationToken = default);
        void Update(Service services);
        void Remove(Service services);

    }
}
