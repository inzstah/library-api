using Microsoft.AspNetCore.Builder;

namespace Library.Infrastructure.Middleware
{
    public static class Middlewares
    {
        public static IApplicationBuilder UseMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            return app;
        }
    }
}
