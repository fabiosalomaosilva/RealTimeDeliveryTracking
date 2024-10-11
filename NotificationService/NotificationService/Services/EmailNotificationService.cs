using Domain;
using NotificationService.Interfaces;
using Postgres.Repositories;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace NotificationService.Services;

public class EmailNotificationService : INotificationService
{
    private readonly ISendGridClient _sendGridClient;
    private readonly INotificationRepository _notificationRepository;

    public EmailNotificationService(ISendGridClient sendGridClient, INotificationRepository notificationRepository)
    {
        _sendGridClient = sendGridClient;
        _notificationRepository = notificationRepository;
    }

    public async Task SendNotification(Delivery delivery, Order order)
    {
        var message = new SendGridMessage
        {
            From = new EmailAddress("no-reply@trackingservice.com", "Tracking Service"),
            Subject = $"Atualização de status da entrega para o pedido {delivery.OrderId}",
            PlainTextContent = $"Olá, {order.CustomerName},\n\nO status da sua entrega foi atualizado para: {delivery.Status}.\nData da última atualização: {delivery.LastUpdated}.\nEndereço: Rua Teste, 689 - Bairro Testando\n\nObrigado por usar nossos serviços!"
        };

        message.AddTo(new EmailAddress("cliente@email.com"));

        var response = await _sendGridClient.SendEmailAsync(message);

        if (response.StatusCode != System.Net.HttpStatusCode.OK)
        {
            Console.WriteLine($"Falha ao enviar e-mail: {response.StatusCode}");
            return;
        }

        var notification = new Notification
        {
            Id = Guid.NewGuid(),
            DeliveryId = delivery.Id,
            Type = "Email",
            Recipient = "cliente@email.com", // Usar e-mail real do cliente
            SentAt = DateTime.UtcNow,
            Success = response.IsSuccessStatusCode,
            ErrorMessage = response.IsSuccessStatusCode ? null : "Falha ao enviar e-mail"
        };

        await _notificationRepository.AddNotificationAsync(notification);
    }
}



