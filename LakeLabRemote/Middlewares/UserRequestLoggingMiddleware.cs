using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LakeLabRemote.DataSource;

namespace LakeLabRemote.Middlewares
{
    public class UserRequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public UserRequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, LoggingDbContext context)
        {
            if (httpContext.User.Identity.IsAuthenticated && httpContext.User.IsInRole("Users"))
            {
                context.UserRequests.Add(new Models.UserRequest(httpContext.User.Identity.Name, DateTime.Now));
                await context.SaveChangesAsync();
            }

            await _next.Invoke(httpContext);
        }
    }
}
