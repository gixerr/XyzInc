using XyzInc.Application.DTO;
using XyzInc.Core.Domain;

namespace XyzInc.Application.Services;

public interface IOrderService
{
    Receipt ProcessOrder(OrderPostDto orderPostDto);
}