using Domain;
using Microsoft.EntityFrameworkCore;

namespace Postgres.Repositories;

public class DeliveryRepository : IDeliveryRepository
{
    private readonly OrderContext _context;

    public DeliveryRepository(OrderContext context)
    {
        _context = context;
    }

    public async Task<Delivery?> GetDeliveryByIdAsync(Guid deliveryId)
    {
        try
        {
            var result = await _context.Deliveries.Where(p => p.Id == deliveryId).FirstOrDefaultAsync();
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Delivery?> GetDeliveryByOrderIdAsync(Guid orderId)
    {
        try
        {
            var result = await _context.Deliveries.Where(p => p.OrderId == orderId).FirstOrDefaultAsync();
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task SaveDeliveryStatus(Order order)
    {
        var delivery = await _context.Deliveries.FirstOrDefaultAsync(d => d.Id == order.Id);
        if (delivery == null)
        {
            delivery = new Delivery
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,
                Status = DeliveryStatus.AwaitingShipment,
                LastUpdated = DateTime.UtcNow,
                History = new List<DeliveryHistory>()
            };

            var history = new DeliveryHistory
            {
                Id = Guid.NewGuid(),
                DeliveryId = delivery.Id,
                Status = delivery.Status,
                Timestamp = DateTime.UtcNow,
                Comments = "Status updated based on Order status."
            };
            delivery.History?.Add(history);

            _context.Deliveries.Add(delivery);
            await _context.SaveChangesAsync();
            return;
        }

        delivery.Status = GetDeliveryStatusFromOrderStatus(order.Status);
        delivery.LastUpdated = DateTime.UtcNow;

        var deliveryHistory = new DeliveryHistory
        {
            Id = Guid.NewGuid(),
            DeliveryId = delivery.Id,
            Status = delivery.Status,
            Timestamp = DateTime.UtcNow,
            Comments = "Status updated based on Order status."
        };
        delivery.History?.Add(deliveryHistory);

        _context.Deliveries.Update(delivery);
        await _context.SaveChangesAsync();
    }

    private static DeliveryStatus GetDeliveryStatusFromOrderStatus(OrderStatus orderStatus)
    {
        return orderStatus switch
        {
            OrderStatus.Pending => DeliveryStatus.AwaitingShipment,
            OrderStatus.Processing => DeliveryStatus.Shipped,
            OrderStatus.Delivering => DeliveryStatus.InTransit,
            OrderStatus.Completed => DeliveryStatus.Delivered,
            OrderStatus.Cancelled => DeliveryStatus.Cancelled,
            _ => DeliveryStatus.AwaitingShipment,
        };
    }
}