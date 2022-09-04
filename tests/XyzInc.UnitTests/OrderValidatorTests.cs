using FluentAssertions;
using XyzInc.Application.Exceptions;
using XyzInc.Application.Validators;
using XyzInc.Core.Domain;

namespace XyzInc.UnitTests;

public class ValidatorTests
{
    [Fact]
    public void order_validator_should_throw_exception_for_invalid_UserId()
    {
        Order order = new()
        {
            UserId = default,
            OrderNumber = 123,
            PayableAmount = 100,
            Description = "test"
        };
        OrderValidator orderValidator = new();

        var validate = () => orderValidator.Validate(order);

        var expectedException = validate.Should().Throw<AppException>();
        expectedException.And.Message.Should()
            .BeEquivalentTo("Provided invalid user ID. User ID cannot be default value of Guid.");
    }
    
    [Fact]
    public void order_validator_should_throw_exception_for_OrderNumber_less_than_0()
    {
        Order order = new()
        {
            UserId = Guid.NewGuid(),
            OrderNumber = -23,
            PayableAmount = 100,
            Description = "test"
        };
        OrderValidator orderValidator = new();

        var validate = () => orderValidator.Validate(order);

        var expectedException = validate.Should().Throw<AppException>();
        expectedException.And.Message.Should()
            .BeEquivalentTo("Invalid order number. Order number must be greater than 0.");
    }
    
    [Fact]
    public void OrderValidator_should_throw_exception_for_OrderNumber_equals_0()
    {
        Order order = new()
        {
            UserId = Guid.NewGuid(),
            OrderNumber = 0,
            PayableAmount = 100,
            Description = "test"
        };
        OrderValidator orderValidator = new();

        var validate = () => orderValidator.Validate(order);

        var expectedException = validate.Should().Throw<AppException>();
        expectedException.And.Message.Should()
            .BeEquivalentTo("Invalid order number. Order number must be greater than 0.");
    }
    
    [Fact]
    public void OrderValidator_should_throw_exception_for_invalid_PayableAmount()
    {
        Order order = new()
        {
            UserId = Guid.NewGuid(),
            OrderNumber = 123,
            PayableAmount = -23,
            Description = "test"
        };
        OrderValidator orderValidator = new();

        var validate = () => orderValidator.Validate(order);

        var expectedException = validate.Should().Throw<AppException>();
        expectedException.And.Message.Should()
            .BeEquivalentTo("Invalid payable amount. Payable amount cannot be less 0.");
    }
    
    [Fact]
    public void OrderValidator_should_not_throw_exception_for_valid_parameters()
    {
        Order order = new()
        {
            UserId = Guid.NewGuid(),
            OrderNumber = 123,
            PayableAmount = 23,
            Description = "test"
        };
        OrderValidator orderValidator = new();

        var validate = () => orderValidator.Validate(order);

        validate.Should().NotThrow<AppException>();
    }
}