using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApp.Middleware
{
    public class ActorControllerLog
    {
        private readonly RequestDelegate _next;

        public ActorControllerLog(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, ILogger<ActorControllerLog> logger)
        {
            if (httpContext.Request.Path.Value.ToLower().Contains("api/actor"))
            {
                logger.LogTrace($"Actor => request:{httpContext.Request.Path}  Method: {httpContext.Request.Path}");
            }
            await _next(httpContext);
        }
    }
}
