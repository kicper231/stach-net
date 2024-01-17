namespace Domain.Model;

public class All : BaseEntity
{
    

    public DateTime PickupDate { get; set; }
    public DateTime DeliveryDate { get; set; }
    public DeliveryStatus DeliveryStatus { get; set; }
}
