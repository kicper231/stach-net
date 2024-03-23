namespace Domain.DTO
{
    public class DeliveryCompanyDTO
    {
        public UserData? User { get; set; }
        public InquiryData Inquiry { get; set; }
        public OfferData Offer { get; set; }
        public DeliveryData Delivery { get; set; }
    }
}