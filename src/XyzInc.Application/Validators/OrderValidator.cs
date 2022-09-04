using System.Runtime.CompilerServices;
using XyzInc.Application.Exceptions;
using XyzInc.Core.Domain;

[assembly: InternalsVisibleTo("XyzInc.UnitTests")]
namespace XyzInc.Application.Validators;

class OrderValidator : IOrderValidator
{
    public void Validate(Order order)
    {
        if (order.UserId.Equals(default))
        {
            throw new AppException("Provided invalid user ID. User ID cannot be default value of Guid.");
        }

        if (order.OrderNumber <= 0)
        {
            throw new AppException("Invalid order number. Order number must be greater than 0.");
        }

        if (order.PayableAmount < 0)
        {
            throw new AppException("Invalid payable amount. Payable amount cannot be less 0.");
        }
    }
}