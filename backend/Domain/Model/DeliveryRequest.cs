namespace Domain.Model;

public class DeliveryRequest : BaseEntity
{
    public int DeliveryRequestId { get; set; }
    public Guid DeliveryRequestPublicId { get; set; }
    public int? UserId { get; set; }
    public User? User { get; set; }
    public string? UserAuth0 { get; set; }
    public int PackageId { get; set; }
    public Package Package { get; set; }
    public int SourceAddressId { get; set; }
    public Address SourceAddress { get; set; }
    public int DestinationAddressId { get; set; }
    public Address DestinationAddress { get; set; }
    public DateTime RequestDate { get; set; }
    public DateTime DeliveryDate { get; set; }
    public DeliveryRequestStatus Status { get; set; }
    public bool WeekendDelivery { get; set; }
    public PackagePriority Priority { get; set; }

    //public bool VipPackage { get; set; }
    //public bool IsCompany { get; set; }
}

public enum DeliveryRequestStatus
{
    Pending,
    Accepted,
    Rejected
}