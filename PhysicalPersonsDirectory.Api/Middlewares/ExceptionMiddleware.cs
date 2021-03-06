﻿
using Microsoft.AspNetCore.Http;
using PhysicalPersonsDirectory.Services.Models.Base;
using PhysicalPersonsDirectory.Services.Services.Abstract;
using System;
using System.Net;
using System.Threading.Tasks;

namespace PhysicalPersonsDirectory.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;                     
        }

        public async Task InvokeAsync(HttpContext httpContext, ILoggerService logger)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                logger.LogError($"Internal Server Error: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error."
            }.ToString());
        }
    }
}
