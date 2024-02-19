using ERP.APIs.Errors;
using System.Net;
using System.Text.Json;

namespace ERP.APIs.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IHostEnvironment env;

        public ExceptionMiddleware(RequestDelegate next ,ILogger<ExceptionMiddleware> logger, IHostEnvironment env)  {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }

        public async Task  InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context); 
            }
           catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                // Log  Exception in Database [Production]
                context.Response.ContentType = "appliction/json";
                context.Response.StatusCode= 500;

                var response = env.IsDevelopment()?
                    new ApiExceptionResponse((int) HttpStatusCode.InternalServerError,ex.Message,ex.StackTrace.ToString())
                    
                   : new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);
               var option = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }; // to make it camelcase  so javaScript can read it

                var json =JsonSerializer.Serialize(response,option);
                
                await  context.Response.WriteAsync(json);  
            }


        }
  
    
    
    }
}
