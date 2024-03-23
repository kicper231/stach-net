using Domain.Model;

namespace Domain.DTO;

public class InquiryCompanyDTO
{
    public UserData? User { get; set; }

    public Guid InquiryId { get; set; }
    public PackageDTO Package { get; set; }

    public AddressDTO SourceAddress { get; set; }

    public AddressDTO DestinationAddress { get; set; }
    public DateTime InquiryDate { get; set; }
    public DateTime DeliveryDate { get; set; }

    public bool WeekendDelivery { get; set; }
    public PackagePriority Priority { get; set; }
}