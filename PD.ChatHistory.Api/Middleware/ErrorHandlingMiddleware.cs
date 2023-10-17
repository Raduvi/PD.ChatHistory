using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace PD.ChatHistory.Api.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError;

            ProblemDetails problem = new()
            {
                Status = (int)code,
                Type = "www.PD.test.com",
                Title = "Server Error",
                Detail = $"An error occurred: {ex.Message}"
            };

            var result = JsonSerializer.Serialize(problem);

            await context.Response.WriteAsync(result);
        }
    }
}
