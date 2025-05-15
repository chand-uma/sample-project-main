using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

public class GlobalExceptionFilter : ExceptionFilterAttribute
{
    public override void OnException(HttpActionExecutedContext context)
    {
        var exception = context.Exception;

        if (exception is ArgumentException)
        {
            context.Response = context.Request.CreateResponse(
                HttpStatusCode.BadRequest,
                new { Error = exception.Message }
            );
        }
        else
        {
            context.Response = context.Request.CreateResponse(
                HttpStatusCode.InternalServerError,
                new { Error = "An unexpected error occurred", Details = exception.Message }
            );
        }
    }
}