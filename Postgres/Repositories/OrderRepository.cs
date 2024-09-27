using Domain;
using Microsoft.EntityFrameworkCore;

namespace Postgres.Repositories;

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

    public Task UpdateStatusOrder(Order order)
    {
        var orderObj = _orderContext.Orders.Find(order.Id);
        if (orderObj == null) return Task.CompletedTask;
        orderObj.Status = order.Status;
        _orderContext.Orders.Update(orderObj);

        return _orderContext.SaveChangesAsync();
    }
}