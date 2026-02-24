using CA_ApplicationLayer.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using CA_ApplicationLayer.Exceptions;

namespace CA_FrameworksDrivers_API.Middelwares
{
    public class ExceptionMiddelware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddelware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidatorException ex)
            {
                await HandleExceptionAsync(context, ex);
            }

        }

        private static  async Task HandleExceptionAsync(HttpContext context, ValidatorException exception)
        {
            // recojo la response
            var response = context.Response;

            // respondo en formato JSON
            response.ContentType = "application/json";

            // establezco el codigo de error
            var statusCode = HttpStatusCode.InternalServerError;

            // creo objeto con eeror y detallae
            var result = JsonSerializer.Serialize(new 
                    { 
                        error = exception.Message,
                        detail = exception.InnerException?.Message
                    }
                );

            response.StatusCode = (int)statusCode;

            await response.WriteAsync(result);

        }   
    }
}
