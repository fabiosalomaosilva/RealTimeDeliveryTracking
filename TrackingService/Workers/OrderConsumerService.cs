using Confluent.Kafka;
using Domain;
using Postgres.Repositories;
using System.Text.Json;

namespace TrackingService.Workers
{
    public class OrderConsumerService : BackgroundService
    {
        private readonly IConsumer<Null, string> _kafkaConsumer;
        private readonly IOrderRepository _orderRepository;
        private readonly IDeliveryRepository _deliveryRepository;

        public OrderConsumerService(IConsumer<Null, string> kafkaConsumer, IOrderRepository orderRepository,
            IDeliveryRepository deliveryRepository)
        {
            _kafkaConsumer = kafkaConsumer;
            _orderRepository = orderRepository;
            _deliveryRepository = deliveryRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _kafkaConsumer.Subscribe("order-events");

            while (!stoppingToken.IsCancellationRequested)
            {
                var consumeResult = _kafkaConsumer.Consume(stoppingToken);
                var order = JsonSerializer.Deserialize<Order>(consumeResult.Message.Value);
                Console.WriteLine("Pedido recebido no OrderConsumerService:\nDados: {consumeResult.Message.Value}");

                // Atualizar status de entrega no banco de dados.
                await UpdateOrderStatusAsync(order);
            }
        }

        private async Task UpdateOrderStatusAsync(Order order)
        {
            await _orderRepository.UpdateStatusOrder(order);
            await _deliveryRepository.SaveDeliveryStatus(order);
        }
    }
}