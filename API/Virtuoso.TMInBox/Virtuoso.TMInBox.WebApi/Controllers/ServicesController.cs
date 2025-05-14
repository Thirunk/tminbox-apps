using Microsoft.AspNetCore.Mvc;
using Virtuoso.TMInBox.Application.Contracts;
using Virtuoso.TMInBox.Application.Services.Service;

namespace Virtuoso.TMInBox.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServicesService _servicesService;

        public ServicesController(IServicesService servicesService)
        {
            _servicesService = servicesService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pageNumber = 0, [FromQuery] int pageSize = 0, bool activeOnly = false)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                var serviceResult = await _servicesService.GetAllAsync(activeOnly);
                if (!serviceResult.IsOk || serviceResult.Value == null)
                    return this.GetActionResult(serviceResult);
                return Ok(serviceResult.Value);
            }
            else
            {
                var serviceResult = await _servicesService.GetAllPaginatedAsync(pageNumber, pageSize);
                if (!serviceResult.IsOk || serviceResult.Value == null)
                    return this.GetActionResult(serviceResult);
                return Ok(serviceResult.Value);
            }
        }
        [Route("byname")]
        [HttpGet]    
       public async Task<IActionResult> GetByName(string name)
        {
            var serviceResult = await _servicesService.GetByNameAsync(name);
            if(!serviceResult.IsOk || serviceResult.Value is null)
                return this.GetActionResult(serviceResult);
            var resultData = serviceResult.Value.ToList();
            return Ok(resultData);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var serviceResult = await _servicesService.GetByIdAsync(id);
            if (!serviceResult.IsOk || serviceResult.Value is null)
                return this.GetActionResult(serviceResult);
            var resultData = serviceResult.Value;
            return Ok(resultData);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ServiceRequest request)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(request);
            }

            var serviceResult = await _servicesService.CreateAsync(request);
            if (!serviceResult.IsOk || serviceResult.Value is null)
                return this.GetActionResult(serviceResult);
            var response = serviceResult.Value;
            return CreatedAtAction(
                actionName: nameof(Post),
                value: new
                {
                    response.Id,
                    response.Name

                });
        }

        [HttpPut]
        public async Task<IActionResult> Put(ServiceRequest request)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            var serviceResult = await _servicesService.UpdateAsync(request);
            return this.GetActionResult(serviceResult);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var serviceResult = await _servicesService.RemoveAsync(id);
            return this.GetActionResult(serviceResult);
        }
    }
}
