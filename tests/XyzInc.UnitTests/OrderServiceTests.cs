using FluentAssertions;
using Moq;
using XyzInc.Application.Dispatchers;
using XyzInc.Application.DTO;
using XyzInc.Application.Exceptions;
using XyzInc.Application.Gateways;
using XyzInc.Application.Services;
using XyzInc.Core.Domain;

namespace XyzInc.UnitTests;

public class OrderServiceTests
{
    private readonly Mock<IPaymentGatewayDispatcher> _paymentGatewayDispatcherMock;
    private readonly Mock<IPaymentGateway> _paymentGatewayMock;

    public OrderServiceTests()
    {
        _paymentGatewayDispatcherMock = new();
        _paymentGatewayMock = new();
    }

    [Fact]
    public void ProcessOrder_should_throw_exception_when_gets_null()
    {
        OrderService orderService = new(_paymentGatewayDispatcherMock.Object);
        
        var processOrder = () => orderService.ProcessOrder(null);

        var expectedException = processOrder.Should().Throw<AppException>();
        expectedException.And.Message.Should()
            .BeEquivalentTo("Order cannot be null.");
    }
    
    [Fact]
    public void ProcessOrder_should_return_Receipt()
    {
        OrderService orderService = new(_paymentGatewayDispatcherMock.Object);
        _paymentGatewayDispatcherMock.Setup(x => x.DispatchGateway(It.IsAny<int>()))
            .Returns(_paymentGatewayMock.Object);
        _paymentGatewayMock.Setup(x => x.ProcessOrder(It.IsAny<Order>())).Returns(new Mock<Receipt>().Object);
        var dto = new OrderPostDto(123, Guid.NewGuid(), 100, 1, "test");
        var receipt = orderService.ProcessOrder(dto);

        receipt.Should().NotBeNull();
        receipt.Description.Should().BeEquivalentTo(default);
        receipt.DateIssued.Should().Be(default);
        receipt.AmountToPay.Should().Be(default);
    }

}