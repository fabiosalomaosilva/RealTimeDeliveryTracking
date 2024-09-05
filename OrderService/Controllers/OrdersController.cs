using Domain;
using Microsoft.AspNetCore.Mvc;
using OrderService.Repositories;
using OrderService.Services;

namespace OrderService.Controllers;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly IOrdersService _orderService;
    private readonly IOrderRepository _orderRepository;

    public OrdersController(IOrdersService orderService, IOrderRepository orderRepository)
    {
        _orderService = orderService;
        _orderRepository = orderRepository;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] Order order)
    {
        await _orderService.CreateOrderAsync(order);
        return Ok(order);
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        var orders = await _orderRepository.GetOrders();
        if (orders != null && orders.Any())
            return Ok(orders);
        return BadRequest();
    }
}
