﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using Webhooks.Models.Dtos;
using Webhooks.Models.Exceptions;

namespace Webhooks.Infrastructure.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, exc.Message);

                await HandleExceptionAsync(httpContext, exc);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            const string contentType = "application/json";

            var statusCode = HttpStatusCode.InternalServerError;
            var message = exception.Message;

            if (exception.GetType() == typeof(InvoiceNotFoundException))
            {
                statusCode = HttpStatusCode.NotFound;
            }

            context.Response.ContentType = contentType;
            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(new ErrorInfoDto {
                StatusCode = context.Response.StatusCode,
                Message = message
            }));
        }
    }
}
