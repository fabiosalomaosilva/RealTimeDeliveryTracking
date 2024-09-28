using Domain;

namespace Postgres.Repositories;

public interface IDeliveryRepository
{
    Task<Delivery?> GetDeliveryByIdAsync(Guid deliveryId);
    Task<Delivery?> GetDeliveryByOrderIdAsync(Guid orderId);
    Task SaveDeliveryStatus(Order order);
}