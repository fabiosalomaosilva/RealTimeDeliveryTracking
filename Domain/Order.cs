namespace Domain
{
    public class Order
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }
        public string DeliveryAddress { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
    }
}
