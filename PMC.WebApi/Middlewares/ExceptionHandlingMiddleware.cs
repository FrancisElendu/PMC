using Newtonsoft.Json;
using PMC.Domain.Exceptions;
using System.Net;

namespace PMC.WebApi.Middlewares
{
    public class ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> _logger) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode code;
            string result;

            // Handle specific exception types
            switch (exception)
            {
                case System.UnauthorizedAccessException _:
                    code = HttpStatusCode.Unauthorized;
                    result = JsonConvert.SerializeObject(new { error = $"Unauthorized access: {exception.Message}" });
                    break;
                case NotFoundException _:
                    code = HttpStatusCode.NotFound;
                    result = JsonConvert.SerializeObject(new { error = $"Resource not found: {exception.Message}" });
                    break;

                case BadRequestException _:
                    code = HttpStatusCode.BadRequest;
                    result = JsonConvert.SerializeObject(new { error = $"Wrong user input: {exception.Message}" });
                    break;

                case InternalServerErrorException _:
                    code = HttpStatusCode.InternalServerError;
                    result = JsonConvert.SerializeObject(new { error = exception.Message });
                    break;
                default:
                    code = HttpStatusCode.InternalServerError;
                    result = JsonConvert.SerializeObject(new { error = "An unexpected error occurred: {exception.Message}" });
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
