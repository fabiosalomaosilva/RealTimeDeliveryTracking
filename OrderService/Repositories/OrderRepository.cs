using Domain;
using Microsoft.EntityFrameworkCore;
using Postgres;

namespace OrderService.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly OrderContext _orderContext;

    public OrderRepository(OrderContext orderContext)
    {
        _orderContext = orderContext;
    }

    public async Task<List<Order>?> GetOrders()
    {
        return await _orderContext.Orders.ToListAsync();
    }

    public async Task AddOrder(Order order)
    {
        try
        {
            _orderContext.Orders.Add(order);
            await _orderContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}