using Microsoft.AspNetCore.Http;
using System;

namespace Book.WebApi.Extensions
{
    public static class HttpContextExtension
    {
        public static bool IsHttpContextValid(this HttpContext httpContext)
        {
            try
            {
                return httpContext is not null && !httpContext.RequestAborted.IsCancellationRequested;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
