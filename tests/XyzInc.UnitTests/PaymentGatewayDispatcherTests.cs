using FluentAssertions;
using Moq;
using XyzInc.Application.Dispatchers;
using XyzInc.Application.Exceptions;

namespace XyzInc.UnitTests;

public class PaymentGatewayDispatcherTests
{
    private readonly Mock<IServiceProvider> _serviceProviderMock;


    public PaymentGatewayDispatcherTests()
    {
        _serviceProviderMock = new();
    }

    [Fact]
    public void dispatcher_should_throw_exception_for_invalid_paymentGatewayId()
    {
        PaymentGatewayDispatcher paymentGatewayDispatcher = new(_serviceProviderMock.Object);
        const int paymentGatewayId = 123;
        
        var dispatchGateway = () => paymentGatewayDispatcher.DispatchGateway(paymentGatewayId);

        var expectedException = dispatchGateway.Should().Throw<AppException>();
        expectedException.And.Message.Should()
            .BeEquivalentTo($"Invalid gateway ID. Provided ID in request: {paymentGatewayId}.");
    }
}