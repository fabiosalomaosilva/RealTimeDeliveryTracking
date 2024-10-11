using Domain;

namespace Postgres.Repositories;

public interface IOrderRepository
{
    Task<Order?> GetOrder(Guid orderId);
    Task<List<Order>?> GetOrders();
    Task AddOrder(Order order);
    Task UpdateStatusOrder(Order order);
}