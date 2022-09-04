using XyzInc.Core.Domain;

namespace XyzInc.Application.Validators;

public interface IOrderValidator
{
    void Validate(Order order);
}