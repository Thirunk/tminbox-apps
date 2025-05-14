using AutoMapper;
using Microsoft.Extensions.Logging;
using Virtuoso.TMInBox.Application.Contracts;
using Virtuoso.TMInBox.Core.Constants;
using Virtuoso.TMInBox.Core.DTOs;
using Virtuoso.TMInBox.Core.Interfaces.Repository;
namespace Virtuoso.TMInBox.Application.Services.Service
{
    public class ServicesService : IServicesService
    {
       private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        ILogger<Virtuoso.TMInBox.Core.Models.Service> _logger;

        public ServicesService(IServiceRepository serviceRepository, IMapper mapper, IUnitOfWork unitOfWork, ILogger<Core.Models.Service> logger)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ServiceResult<ServiceResponse>> CreateAsync(ServiceRequest entity)
        {
            try
            {
                Core.Models.Service service =_mapper.Map<ServiceRequest,Core.Models.Service>(entity);
                await _serviceRepository.AddAsync(service);
                await _unitOfWork.SaveChangesAsync();
                ServiceResponse response = _mapper.Map<Core.Models.Service,ServiceResponse>(service);
                return new ServiceResult<ServiceResponse>(response, ServiceResultType.Ok);
            }
            catch (Exception ex) {
                _logger.LogError(ex, ErrorConstants.ServiceFailed);
                return null;
            }
        }
        
        public Task<ServiceResult<IEnumerable<ServiceResponse>>> GetAllAsync(bool activeOnly = false)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<PagedResult<ServiceResponse>>> GetAllPaginatedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<ServiceResponse>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<IEnumerable<ServiceResponse>>> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> UpdateAsync(ServiceRequest entity)
        {
            throw new NotImplementedException();
        }
    }
}
