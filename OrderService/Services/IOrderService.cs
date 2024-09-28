using Domain;

namespace OrderService.Services;

public interface IOrdersService
{
    Task CreateOrderAsync(Order order);
}