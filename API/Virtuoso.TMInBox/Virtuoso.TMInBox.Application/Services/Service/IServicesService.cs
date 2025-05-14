using Virtuoso.TMInBox.Application.Contracts;
using Virtuoso.TMInBox.Core.DTOs;

namespace Virtuoso.TMInBox.Application.Services.Service
{
    public interface IServicesService
    {
        Task<ServiceResult<ServiceResponse>> CreateAsync(ServiceRequest entity);
        Task<ServiceResult<IEnumerable<ServiceResponse>>> GetAllAsync(bool activeOnly = false);
        Task<ServiceResult<PagedResult<ServiceResponse>>> GetAllPaginatedAsync(
            int pageNumber,int pageSize,CancellationToken cancellationToken=default);
        Task<ServiceResult<IEnumerable<ServiceResponse>>> GetByNameAsync(string name);
        Task<ServiceResult<ServiceResponse>> GetByIdAsync(int id);
        Task<ServiceResult> RemoveAsync(int id);
        Task<ServiceResult> UpdateAsync(ServiceRequest entity);
    }
}
