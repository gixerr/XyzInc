using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using XyzInc.Application.Dispatchers;
using XyzInc.Application.Exceptions;
using XyzInc.Application.Gateways;
using XyzInc.Application.Services;
using XyzInc.Application.Validators;

namespace XyzInc.Application;

public static class Extensions
{
    public static void AddApplication(this IServiceCollection services)
        => services
            .AddScoped<IOrderService, OrderService>()
            .AddScoped<IPaymentGatewayDispatcher, PaymentGatewayDispatcher>()
            .AddScoped<IOrderValidator, OrderValidator>()
            .AddScoped<EuropePaymentGateway>()
            .AddScoped<UnitedStatesPaymentGateway>();


    public static void AddErrorHandling(this IServiceCollection services)
        => services
            .AddScoped<ErrorHandlerMiddleware>()
            .AddSingleton<IExceptionToResponseMapper, ExceptionToResponseMapper>();

    public static void UseErrorHandling(this IApplicationBuilder app)
        => app
            .UseMiddleware<ErrorHandlerMiddleware>();

}