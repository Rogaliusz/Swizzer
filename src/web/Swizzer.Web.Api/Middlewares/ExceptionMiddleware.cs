using Microsoft.AspNetCore.Http;
using Swizzer.Shared.Common.Exceptions;
using Swizzer.Shared.Common.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Swizzer.Web.Api.Middlewares
{
    public class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            } 
            catch (Exception ex)
            {
                HandleException(ex, context, next);
            }
        }

        private void HandleException(Exception ex, HttpContext context, RequestDelegate next)
        {
            var message = ServerErrorCodes.UnauthorizedAccess;
            var statusCode = HttpStatusCode.InternalServerError;

            switch (ex) 
            {
                case SwizzerServerException swizzer:
                    statusCode = GetStatusCode(swizzer);
                    message = swizzer.ErrorCode;
                    break;
            }
            var error = new ErrorDto
            {
                Exception = ex,
                ErrorCode = message
            };

            var json = SwizzerJsonSerializer.Serialize(error);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) statusCode;

            context.Response.WriteAsync(json);
        }

        private HttpStatusCode GetStatusCode(SwizzerServerException swizzer)
        {
            switch (swizzer.ErrorCode)
            {
                case ServerErrorCodes.UnauthorizedAccess:
                    return HttpStatusCode.Unauthorized;
                default:
                    return HttpStatusCode.BadRequest;
            }
        }
    }
}
