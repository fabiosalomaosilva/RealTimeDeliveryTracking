using Domain;

namespace Postgres.Repositories;
public interface INotificationRepository
{
    Task AddNotificationAsync(Notification notification);
    Task<IEnumerable<Notification>> GetNotificationsByDeliveryIdAsync(Guid deliveryId);
}