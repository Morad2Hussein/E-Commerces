

using Azure;
using Microsoft.AspNetCore.Http;

namespace E_CommerceWeb.MiddleWare
{
    public class GlobalExceptionHandlingMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlingMiddleWare> _logger;
        public GlobalExceptionHandlingMiddleWare(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleWare> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
                if (context.Response.StatusCode == StatusCodes.Status404NotFound)
                 await HandleNotFoundApiAsync(context);


            }
            catch (Exception ex)
            {
                _logger.LogError($"An unhandled exception occurred :{ex.Message}");
                await HandleExceptionAsync(context, ex);
            }
        }

        #region HandleExceptionAsync
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var errorDetails = new ErrorDetails
            {
                Message = exception.Message
            };
            context.Response.StatusCode = exception switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                UnauthorizedException => StatusCodes.Status401Unauthorized,
                ValidationException validationException => HandleValidtionException(validationException, errorDetails),
                _ => StatusCodes.Status500InternalServerError
            };
            errorDetails.StatusCode = context.Response.StatusCode;
            await context.Response.WriteAsync(errorDetails.ToString());



        }
        #endregion
        #region HandleNotFoundApiAsync
        private async Task HandleNotFoundApiAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            var errorDetails = new ErrorDetails
            {
                StatusCode = StatusCodes.Status404NotFound,
                Message = $"The Endpoint  With Url {context.Request.Path} not found"
            }.ToString();
            await context.Response.WriteAsync(errorDetails);
        }
        #endregion
        #region HandleValidtionException
        private  int HandleValidtionException(ValidationException validationException , ErrorDetails errorDetails)
        {
            errorDetails.Errors = validationException.Errors;

           return StatusCodes.Status400BadRequest;

        }
        #endregion
    }


}
