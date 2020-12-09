using Microsoft.AspNetCore.Builder;


namespace MoviesApp.Middleware
{
    public static class MiddlewareExtension
    {
        public static IApplicationBuilder UseRequestLog(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ActorControllerLog>();
        }
    }
}
