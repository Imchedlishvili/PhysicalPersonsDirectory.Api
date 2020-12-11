using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhysicalPersonsDirectory.Api.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errorsInMOdelState = context.ModelState.Where(t => t.Value.Errors.Count > 0)
                                                .ToDictionary(k => k.Key, v => v.Value.Errors.Select(e => e.ErrorMessage)).ToArray();

                //var errorResponse = new ResponseBaseModel();
                //foreach (var error in errorsInMOdelState)
                //{
                //    foreach (var subError in error.Value)
                //    {
                //        var errorModel = new ErrorModel
                //        {
                //            FieldName = error.Key,
                //            Message = subError
                //        };

                //        errorResponse.ErrorMessage.Add(errorModel);
                //    }
                //}

                //context.Result = new BadRequestObjectResult(errorResponse);
                return;
            }

            await next();
        }

    }
}
