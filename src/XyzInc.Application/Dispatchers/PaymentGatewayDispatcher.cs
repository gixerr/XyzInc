using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using XyzInc.Application.Exceptions;
using XyzInc.Application.Gateways;

[assembly: InternalsVisibleTo("XyzInc.UnitTests")]
namespace XyzInc.Application.Dispatchers;

internal class PaymentGatewayDispatcher : IPaymentGatewayDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    private readonly Dictionary<int, Func<IPaymentGateway>> _paymentGatewayDispatch;

    public PaymentGatewayDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _paymentGatewayDispatch = new Dictionary<int, Func<IPaymentGateway>>()
        {
            [1] = GetPaymentGateway<EuropePaymentGateway>,
            [2] = GetPaymentGateway<UnitedStatesPaymentGateway>
        };
    }
   
    public IPaymentGateway DispatchGateway(int paymentGatewayId)
    {
        if (!_paymentGatewayDispatch.ContainsKey(paymentGatewayId))
        {
            throw new AppException($"Invalid gateway ID. Provided ID in request: {paymentGatewayId}.");
        }
        return _paymentGatewayDispatch[paymentGatewayId].Invoke();
    }

    private IPaymentGateway GetPaymentGateway<T>() where T : IPaymentGateway
    {
        using var scope = _serviceProvider.CreateScope();
        var gateway = scope.ServiceProvider.GetRequiredService<T>();
        
        return gateway;
    }
    
}