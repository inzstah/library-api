using Library.Application.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Library.Infrastructure.Middleware
{
    public class ExceptionHandlerMiddleware(RequestDelegate next)
    {
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (Exception ex)
        {
            var code = StatusCode(ex);

            if (code == 0)
            {
                throw;
            }

            context.Response.StatusCode = code;
            context.Response.ContentType = "text/plain";

            await context.Response.WriteAsync(ex.Message);
        }
    }

    static int StatusCode(Exception ex)
    {
        return ex switch
        {
            BadRequestException => 400,
            UnauthorizedException => 401,
            ForbiddenException => 403,
            NotFoundException => 404,
            TimeoutException => 408,
            AlreadyExistsException => 409,
            _ => 0,
        };
    }
}
}
