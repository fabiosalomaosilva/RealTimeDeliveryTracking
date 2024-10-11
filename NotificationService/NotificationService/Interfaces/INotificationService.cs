using Domain;

namespace NotificationService.Interfaces;

public interface INotificationService
{
    Task SendNotification(Delivery delivery, Order order);
}