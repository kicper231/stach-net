namespace Domain.Model;


public class Offer : BaseEntity
{
    public int OfferId { get; set; }
    public int DeliveryRequestId { get; set; }
    public DeliveryRequest DeliveryRequest { get; set; }
    public string InquiryId { get; set; }
    public int CourierCompanyId { get; set; }
    public CourierCompany CourierCompany { get; set; }

    public double totalPrice { get; set; }
    public DateTime OfferValidity { get; set; }
    public OfferStatus OfferStatus { get; set; }
}

public enum OfferStatus
{
    Available,
    Expired,
    Accepted
}

