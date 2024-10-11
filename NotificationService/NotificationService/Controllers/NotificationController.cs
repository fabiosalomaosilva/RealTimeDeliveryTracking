using Microsoft.AspNetCore.Mvc;
using NotificationService.Interfaces;
using Postgres.Repositories;

namespace NotificationService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DeliveryController : ControllerBase
{
    private readonly IDeliveryRepository _deliveryRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly INotificationService _notificationService;
    private readonly INotificationRepository _notificationRepository;

    public DeliveryController(IDeliveryRepository deliveryRepository, IOrderRepository orderRepository, INotificationService notificationService, INotificationRepository notificationRepository)
    {
        _deliveryRepository = deliveryRepository;
        _orderRepository = orderRepository;
        _notificationService = notificationService;
        _notificationRepository = notificationRepository;
    }

    [HttpPost("{orderId}/sendnotification")]
    public async Task<IActionResult> SendNotification(Guid orderId)
    {
        var delivery = await _deliveryRepository.GetDeliveryByOrderIdAsync(orderId);
        if (delivery == null)
        {
            return NotFound();
        }

        var order = await _orderRepository.GetOrder(orderId);

        // Enviar notificações
        await _notificationService.SendNotification(delivery, order);

        return Ok();
    }

    [HttpGet("{deliveryId}/notifications")]
    public async Task<IActionResult> GetNotifications(Guid deliveryId)
    {
        var notifications = await _notificationRepository.GetNotificationsByDeliveryIdAsync(deliveryId);
        return Ok(notifications);
    }
}

