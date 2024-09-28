using Confluent.Kafka;
using Domain;
using Postgres.Repositories;
using System.Text.Json;

namespace OrderService.Services;

public class OrdersService : IOrdersService
{
    private readonly IProducer<Null, string> _kafkaProducer;
    private readonly IOrderRepository _orderRepository;

    public OrdersService(IProducer<Null, string> kafkaProducer, IOrderRepository orderRepository)
    {
        _kafkaProducer = kafkaProducer;
        _orderRepository = orderRepository;
    }

    public async Task CreateOrderAsync(Order order)
    {
        // Lógica para salvar o pedido no banco de dados
        await _orderRepository.AddOrder(order);

        var orderCreatedEvent = JsonSerializer.Serialize(order);

        // Publicando evento no Kafka
        await _kafkaProducer.ProduceAsync("order-events", new Message<Null, string> { Value = orderCreatedEvent });
    }
}