namespace Domain.Model;

public class deliveryRequest : BaseEntity
{
    public int deliveryRequestId { get; set; }
    public Guid deliveryRequestPublicId {  get; set; }
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
    public deliveryRequestStatus Status { get; set; }
    public bool WeekendDelivery { get; set; }
    public PackagePriority Priority { get; set; }

    //public bool VipPackage { get; set; }  
    //public bool IsCompany { get; set; }   
}

public enum deliveryRequestStatus
{
    Pending,
    Accepted,
    Rejected
}