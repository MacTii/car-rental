using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace API.Middlewares
{
    public class GlobalExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger)
            => _logger = logger;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (ArgumentException e)
            {
                await HandleExceptionAsync(context, e, HttpStatusCode.BadRequest, "Bad request", e.Message); // 400
            }
            catch(UnauthorizedAccessException e)
            {
                await HandleExceptionAsync(context, e, HttpStatusCode.Unauthorized, "Unauthorized", e.Message); // 401
            }
            catch (InvalidOperationException e)
            {
                await HandleExceptionAsync(context, e, HttpStatusCode.NotFound, "Not found", e.Message); // 404
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e, HttpStatusCode.InternalServerError, "Internal server error",
                    "An internal server has occurred."); // 500
            }
        }

        private static ProblemDetails CreateProblemDetails(int statusCode, string title, string detail)
        {
            return new ProblemDetails
            {
                Status = statusCode,
                Type = "https://httpstatuses.com/" + statusCode,
                Title = title,
                Detail = detail
            };
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode statusCode,
            string title, string detail)
        {
            _logger.LogError(exception, detail);

            context.Response.StatusCode = (int)statusCode;

            var problem = CreateProblemDetails(context.Response.StatusCode, title, detail);
            string json = JsonSerializer.Serialize(problem);

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(json);
        }
    }
}
