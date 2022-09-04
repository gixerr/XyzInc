using FluentAssertions;
using Moq;
using XyzInc.Application.Exceptions;
using XyzInc.Application.Gateways;
using XyzInc.Application.Validators;
using XyzInc.Core.Domain;

namespace XyzInc.UnitTests;

public class PaymentGatewayTests
{
    private readonly Mock<IOrderValidator> _orderValidatorMock;

    public PaymentGatewayTests()
    {
        _orderValidatorMock = new();
    }
    
    [Fact]
    public void EuropePaymentGateway_should_throw_exception_for_null_order()
    {
        EuropePaymentGateway europePaymentGateway = new(_orderValidatorMock.Object);

        var processOrder = () => europePaymentGateway.ProcessOrder(null);

        var expectedException = processOrder.Should().Throw<AppException>();
        expectedException.And.Message.Should()
            .BeEquivalentTo("Gateway received null order. Order cannot be null.");
    }
    
    [Fact]
    public void UnitedStates_should_throw_exception_for_null_order()
    {
        UnitedStatesPaymentGateway europePaymentGateway = new(_orderValidatorMock.Object);

        var processOrder = () => europePaymentGateway.ProcessOrder(null);

        var expectedException = processOrder.Should().Throw<AppException>();
        expectedException.And.Message.Should()
            .BeEquivalentTo("Gateway received null order. Order cannot be null.");
    }
    
    [Fact]
    public void EuropePaymentGateway_should_return_receipt_after_success_validation()
    {
        EuropePaymentGateway europePaymentGateway = new(_orderValidatorMock.Object);
        Order order = new()
        {
            PayableAmount = 100,
            Description = "test"
        };
        _orderValidatorMock.Setup(x => x.Validate(It.IsAny<Order>())).Verifiable();

        var receipt =  europePaymentGateway.ProcessOrder(order);

        receipt.Should().NotBeNull();
        receipt.Description.Should().BeEquivalentTo(order.Description);
        receipt.AmountToPay.Should().Be(order.PayableAmount);
        receipt.DateIssued.Should().NotBe(default);
    }
    
    [Fact]
    public void UnitedStatesPaymentGateway_should_return_receipt_after_success_validation()
    {
        UnitedStatesPaymentGateway unitedStatesPaymentGateway = new(_orderValidatorMock.Object);
        Order order = new()
        {
            PayableAmount = 100,
            Description = "test"
        };
        _orderValidatorMock.Setup(x => x.Validate(It.IsAny<Order>())).Verifiable();

        var receipt =  unitedStatesPaymentGateway.ProcessOrder(order);

        receipt.Should().NotBeNull();
        receipt.Description.Should().BeEquivalentTo(order.Description);
        receipt.AmountToPay.Should().Be(order.PayableAmount);
        receipt.DateIssued.Should().NotBe(default);
    }
    
    
}