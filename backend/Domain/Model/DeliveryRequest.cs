

namespace Domain.Model;



public class DeliveryRequest : BaseEntity
{
    public int RequestId { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public int PackageId { get; set; }
    public Package Package { get; set; }
    public int SourceAddressId { get; set; }
    public Address SourceAddress { get; set; }
    public int DestinationAddressId { get; set; }
    public Address DestinationAddress { get; set; }
    public DateTime RequestDate { get; set; }
    public DateTime DeliveryDate { get; set; }
    public DeliveryRequestStatus Status { get; set; }


}

public enum DeliveryRequestStatus
{
    Pending,
    Accepted,
    Rejected
}
