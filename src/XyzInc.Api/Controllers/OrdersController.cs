using Microsoft.AspNetCore.Mvc;
using XyzInc.Application.DTO;
using XyzInc.Application.Services;

namespace XyzInc.Api.Controllers;

public class OrdersController : BaseController
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }
    
    [HttpPost]
    public ActionResult ProcessOrder(OrderPostDto orderPostDto)
    {
        var receipt = _orderService.ProcessOrder(orderPostDto);

        return Ok(receipt);
    }
}