using Domain;
using FirebaseAdmin.Messaging;
using NotificationService.Interfaces;
using Postgres.Repositories;

namespace NotificationService.Services;

public class PushNotificationService : INotificationService
{
    private readonly FirebaseMessaging _firebaseMessaging;
    private readonly INotificationRepository _notificationRepository;

    public PushNotificationService(FirebaseMessaging firebaseMessaging, INotificationRepository notificationRepository)
    {
        _firebaseMessaging = firebaseMessaging;
        _notificationRepository = notificationRepository;
    }

    public async Task SendNotification(Delivery delivery, Order order)
    {
        var message = new Message
        {
            Notification = new FirebaseAdmin.Messaging.Notification
            {
                Title = "Atualização de Pedido",
                Body = $"Olá, {order.CustomerName}, o status do seu pedido foi atualizado para: {delivery.Status}"
            },
            Token = "device_token_here"
        };

        var response = await _firebaseMessaging.SendAsync(message);
        Console.WriteLine($"Push notification enviada com sucesso: {response}");

        var notification = new Domain.Notification
        {
            Id = Guid.NewGuid(),
            DeliveryId = delivery.Id,
            Type = "Push",
            Recipient = "device_token_here",
            SentAt = DateTime.UtcNow,
            Success = true,
            ErrorMessage = response != null ? "Falha ao enviar push notification" : null
        };

        await _notificationRepository.AddNotificationAsync(notification);
    }
}