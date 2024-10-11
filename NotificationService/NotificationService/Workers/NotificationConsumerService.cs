using Confluent.Kafka;
using Domain;
using NotificationService.Interfaces;
using Postgres.Repositories;
using System.Text.Json;

namespace NotificationService.Workers;
public class NotificationConsumerService : BackgroundService
{
    private readonly IConsumer<Null, string> _kafkaConsumer;
    private readonly INotificationService _notificationService;
    private readonly IOrderRepository _orderRepository;

    public NotificationConsumerService(IConsumer<Null, string> kafkaConsumer, INotificationService notificationService, IOrderRepository orderRepository)
    {
        _kafkaConsumer = kafkaConsumer;
        _notificationService = notificationService;
        _orderRepository = orderRepository;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _kafkaConsumer.Subscribe("delivery-updates");
        while (!stoppingToken.IsCancellationRequested)
        {
            var consumeResult = _kafkaConsumer.Consume(stoppingToken);
            var delivery = JsonSerializer.Deserialize<Delivery>(consumeResult.Message.Value);
            Console.WriteLine($"Notificação recebida para Pedido {delivery.OrderId}: {delivery.Status}");
            var order = await _orderRepository.GetOrder(delivery.OrderId);

            // Enviar notificações
            await _notificationService.SendNotification(delivery, order);
        }
    }
}