using XyzInc.Application.Dispatchers;
using XyzInc.Application.DTO;
using XyzInc.Application.Exceptions;
using XyzInc.Core.Domain;

namespace XyzInc.Application.Services;

internal class OrderService : IOrderService
{
    private readonly IPaymentGatewayDispatcher _paymentGatewayDispatcher;

    public OrderService(IPaymentGatewayDispatcher paymentGatewayDispatcher)
    {
        _paymentGatewayDispatcher = paymentGatewayDispatcher;
    }
    public Receipt ProcessOrder(OrderPostDto orderPostDto)
    {
        if (orderPostDto is null)
        {
            throw new AppException("Order cannot be null.");
        }
        var paymentGateway = _paymentGatewayDispatcher.DispatchGateway(orderPostDto.PaymentGatewayId);
        var order = MapToOrder(orderPostDto);
        var receipt = paymentGateway.ProcessOrder(order);

        return receipt;
    }
    
    private Order MapToOrder(OrderPostDto dto)
        => new()
        {
            OrderNumber = dto.OrderNumber,
            UserId = dto.UserId,
            PayableAmount = dto.PayableAmount,
            Description = dto.Description
        };
}