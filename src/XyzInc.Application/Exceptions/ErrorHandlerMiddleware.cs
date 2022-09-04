using System.Net;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace XyzInc.Application.Exceptions;

public class ErrorHandlerMiddleware : IMiddleware
{
    private readonly IExceptionToResponseMapper _exceptionToResponseMapper;

    public ErrorHandlerMiddleware(IExceptionToResponseMapper exceptionToResponseMapper)
    {
        _exceptionToResponseMapper = exceptionToResponseMapper;
    }
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            await HandleErrorAsync(context, exception);
        }
    }
    
    private async Task HandleErrorAsync(HttpContext context, Exception exception)
    {
        var errorResponse = _exceptionToResponseMapper.Map(exception);
        context.Response.StatusCode = (int)(errorResponse?.StatusCode ?? HttpStatusCode.InternalServerError);
        var response = errorResponse?.Response;
        var payload = JsonConvert.SerializeObject(response);
        if (response is null)
        {
            return;
        }

        await context.Response.WriteAsync(payload);
    }

  
}