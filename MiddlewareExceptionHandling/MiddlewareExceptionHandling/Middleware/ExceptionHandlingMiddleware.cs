using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MiddlewareExceptionHandling.Exception;
using System.Net;
using System.Threading.Tasks;

namespace MiddlewareExceptionHandling.Middleware
{
    //middleware-stack of code blocks which can run one after another
    public class ExceptionHandlingMiddleware : IMiddleware
    {
       
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            //RequestDelegate has the next middleware inside the pipeline
            //context gives context on http request
            try
            {
                await next(context);
            }
            catch(DomainNotFoundException e) 
            {
                
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await context.Response.WriteAsync(e.Message);

            }
            catch (System.Exception e)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync(e.Message);


            }

        }
    }
}
