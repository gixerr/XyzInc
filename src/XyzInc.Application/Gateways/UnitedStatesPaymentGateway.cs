using XyzInc.Application.Validators;
using XyzInc.Core.Domain;

namespace XyzInc.Application.Gateways;

internal class UnitedStatesPaymentGateway : IPaymentGateway
{
    private readonly IOrderValidator _orderValidator;

    public UnitedStatesPaymentGateway(IOrderValidator orderValidator)
    {
        _orderValidator = orderValidator;
    }
    public Receipt ProcessOrder(Order order)
    {
        if (order is null)
        {
            throw new AppException("Gateway received null order. Order cannot be null.");
        }
        _orderValidator.Validate(order);

        Receipt receipt = new()
        {
            DateIssued = DateTime.UtcNow,
            AmountToPay = order.PayableAmount
        };

        return receipt;
    }
}