namespace Domain.Model;

public class Delivery : BaseEntity
{
    public int DeliveryId { get; set; }
    public Guid PublicID { get; set; }
    public int OfferId { get; set; }
    public Offer Offer { get; set; }
    public int? CourierId { get; set; }
    public User? Courier { get; set; }
    public Guid ApiId { get; set; }
    public DateTime PickupDate { get; set; }
    public DateTime DeliveryDate { get; set; }
    public DeliveryStatus DeliveryStatus { get; set; }
}

//public enum DeliveryStatus
//{
//    PickedUp,
//    Delivered,
//    Failed,
//     accepted,
//     AcceptedByKurier,
//     cancelled,
//     CannotDelivery,
//     CancelledByWorker,
//     WaitingToAcceptByWorker

//}

public enum DeliveryStatus
{
    nostatus,
    accepted,
    rejected,
    acceptedbycourier,
    pickedup,
    delivered,
    cannotdelivery,
    cancelled,
}