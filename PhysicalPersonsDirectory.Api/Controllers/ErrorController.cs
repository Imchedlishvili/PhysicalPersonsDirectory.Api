using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PhysicalPersonsDirectory.Services.Models.Base;
using PhysicalPersonsDirectory.Services.Services.Abstract;
using System.Net;

namespace PhysicalPersonsDirectory.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        private readonly ILoggerService _loggerService;
        public ErrorController(ILoggerService loggerService)
        {
            _loggerService = loggerService;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/error")]
        public void Error()
        {
            HttpContext.Response.ContentType = "application/json";
            HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            HttpContext.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = HttpContext.Response.StatusCode,
                Message = "Internal Server Error."
            }.ToString());

            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            var errorMessage = JsonConvert.SerializeObject(new { exceptionDetails.Error, exceptionDetails.Path, exceptionDetails.Error.StackTrace });
            _loggerService.LogError(errorMessage);
        }
    }
}
