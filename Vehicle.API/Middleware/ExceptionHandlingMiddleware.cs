using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;
using Vehicle.Application.Common.Exceptions;

namespace Vehicle.API.Middleware
{

    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning($"Not Found: {ex.Message}");
                await HandleNotFoundExceptionAsync(context, ex);
            }
            //catch (SqlException ex) {
            //    if (ex.Number == 2627 || ex.Number == 2601)
            //    {
            //        context.Response.StatusCode = StatusCodes.Status409Conflict;
            //        await context.Response.WriteAsync("Duplicate key error. Conflict with the current state of the resource.");
            //    }
            //}
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleNotFoundExceptionAsync(HttpContext context, NotFoundException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;

            var result = new
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message,
                Detail = "The specified resource was not found."
            };

            return context.Response.WriteAsJsonAsync(result);
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var result = new
            {
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error from the middleware.",
                Detail = exception.Message + ":( " + exception.InnerException + ")"
            };

            return context.Response.WriteAsJsonAsync(result);
        }
    }

}
