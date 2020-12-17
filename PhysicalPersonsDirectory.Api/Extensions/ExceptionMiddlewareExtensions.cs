using Microsoft.AspNetCore.Builder;
using PhysicalPersonsDirectory.Api.Middlewares;

namespace PhysicalPersonsDirectory.Api.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
