using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class Delivery
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid OrderId { get; set; }
    public DeliveryStatus Status { get; set; } = DeliveryStatus.AwaitingShipment;
    public DateTime LastUpdated { get; set; }

    public virtual List<DeliveryHistory>? History { get; set; }
}