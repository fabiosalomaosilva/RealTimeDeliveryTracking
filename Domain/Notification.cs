namespace Domain;

public class Notification
{
    public Guid Id { get; set; }
    public Guid DeliveryId { get; set; }
    public string Type { get; set; }
    public string Recipient { get; set; }
    public DateTime SentAt { get; set; }
    public bool Success { get; set; }
    public string ErrorMessage { get; set; }
}
