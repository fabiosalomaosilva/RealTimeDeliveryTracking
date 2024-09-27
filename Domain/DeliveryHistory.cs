using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain;

public class DeliveryHistory
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; } = Guid.NewGuid();

    public DeliveryStatus Status { get; set; }
    public DateTime Timestamp { get; set; }
    public string? Comments { get; set; }
    public Guid DeliveryId { get; set; }
    [JsonIgnore] public Delivery? Delivery { get; set; }
}