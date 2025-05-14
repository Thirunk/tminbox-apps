using Microsoft.AspNetCore.Mvc;
using Virtuoso.TMInBox.Application.Contracts;
using Virtuoso.TMInBox.Core.DTOs;

namespace Virtuoso.TMInBox.WebApi.Controllers
{
    public static class ServiceActionResult
    {
        public static ActionResult GetActionResult(this ControllerBase controller, ServiceResult serviceResult)
        {
            if(serviceResult.IsBadRequest)
                return controller.BadRequest(serviceResult.Message);
            if(serviceResult.IsNotFound)
                return controller.NotFound(serviceResult.Message);
            if(serviceResult.IsError)
                return controller.Problem(serviceResult.Message);
            return controller.Ok();
        }
    }
}
