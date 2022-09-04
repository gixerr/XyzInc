using XyzInc.Core.Domain;

namespace XyzInc.Application.Gateways;

public interface IPaymentGateway
{
    Receipt ProcessOrder(Order order);
}