using XyzInc.Application.Gateways;

namespace XyzInc.Application.Dispatchers;

public interface IPaymentGatewayDispatcher
{
    IPaymentGateway DispatchGateway(int paymentGatewayId);
}