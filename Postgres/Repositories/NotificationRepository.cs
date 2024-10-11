using Domain;
using Microsoft.EntityFrameworkCore;

namespace Postgres.Repositories;

public class NotificationRepository : INotificationRepository
{
    private readonly OrderContext _context;

    public NotificationRepository(OrderContext context)
    {
        _context = context;
    }

    public async Task AddNotificationAsync(Notification notification)
    {
        _context.Notifications.Add(notification);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Notification>> GetNotificationsByDeliveryIdAsync(Guid deliveryId)
    {
        return await _context.Notifications.Where(n => n.DeliveryId == deliveryId).ToListAsync();
    }
}
