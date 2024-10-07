﻿using System.Net;
using System.Text.Json;
using Library.Application.Exceptions;
using Microsoft.AspNetCore.Http;
namespace Library.Infrastructure.Middleware
{
    public class ExceptionHandlerMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = string.Empty;
            switch (ex)
            {
                /*case CustomValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(validationException.Message);
                    break;*/
               /* case NoElementException NoElementsException:
                    code = HttpStatusCode.NotFound;
                    result = JsonSerializer.Serialize(NoElementsException.Message);
                    break;*/
                /*case Exception exc:
                    code = HttpStatusCode.NotImplemented;
                    result = JsonSerializer.Serialize(exc.Message);
                    break;*/
            }
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            if (result == string.Empty)
            {
                result = JsonSerializer.Serialize(new { error = ex.Message });
            }
            return context.Response.WriteAsync(result);
        }
    }
}
