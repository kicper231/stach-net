using Domain.Model;

namespace Domain.DTO
{
    public class DTOIOD
    {
        public UserData? User { get; set; }
        public InquiryData Inquiry { get; set; }
        public OfferData Offer { get; set; }
        public DeliveryData Delivery { get; set; }
    }

    public class InquiryData
    {
        // public UserDTO? User { get; set; }

        public Guid InquiryId { get; set; }
        public PackageDTO Package { get; set; }

        public AddressDTO SourceAddress { get; set; }

        public AddressDTO DestinationAddress { get; set; }
        public DateTime InquiryDate { get; set; }
        public DateTime DeliveryDate { get; set; }

        public bool WeekendDelivery { get; set; }
        public PackagePriority Priority { get; set; }
    }

    public class OfferData
    {
        public double totalPrice { get; set; }
        public string Currency { get; set; }
        // public string CompanyName { get; set; }
    }

    public class DeliveryData
    {
        // Właściwości związane z dostawą

        public Guid DeliveryId { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string DeliveryStatus { get; set; }
        public UserData? Courier { get; set; }

        // Dodaj inne właściwości związane z dostawą
    }

    public class UserData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}