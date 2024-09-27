using Domain;

namespace Postgres.Repositories;

public interface IOrderRepository
{
    Task<List<Order>?> GetOrders();
    Task AddOrder(Order order);
    Task UpdateStatusOrder(Order order);
}