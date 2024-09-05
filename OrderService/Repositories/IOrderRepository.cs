using Domain;

namespace OrderService.Repositories;

public interface IOrderRepository
{
    Task<List<Order>?> GetOrders();
    Task AddOrder(Order order);

}