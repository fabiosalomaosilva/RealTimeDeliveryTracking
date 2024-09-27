using Microsoft.AspNetCore.Mvc;
using Postgres.Repositories;
using System.Text.Json;

namespace TrackingService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DeliveryController : ControllerBase
{
    private readonly IDeliveryRepository _repository;

    public DeliveryController(IDeliveryRepository repository)
    {
        _repository = repository;
    }


    [HttpGet("GetDeliveryStatus")]
    public async Task<IActionResult> GetDeliveryStatus(Guid deliveryId)
    {
        var delivery = await _repository.GetDeliveryByIdAsync(deliveryId);
        if (delivery != null)
            return Ok(JsonSerializer.Serialize(delivery));

        return NotFound();
    }

    [HttpGet("GetDeliveryStatusByOrderId")]
    public async Task<IActionResult> GetDeliveryStatusByOrderId(Guid orderId)
    {
        var delivery = await _repository.GetDeliveryByOrderIdAsync(orderId);
        if (delivery != null)
            return Ok(JsonSerializer.Serialize(delivery));

        return NotFound();
    }
}