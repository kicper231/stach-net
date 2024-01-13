using Domain.Model;

namespace Domain.DTO;



public class OfferDTO
{
    public string Auth0Id { get; set; }
    public string InquiryId { get; set; }
    public string CompanyName { get; set; }

    public string Name { get; set; }
    public string Email { get; set; }
    public AddressDTO Address { get; set; }
}



public class OfferOurApiRespondDTO
{
    public Guid OfferRequestId { get; set; }
   // public DateTime ValidTo { get; set; }
}

public class OfferOurApiDTO
{
    public string Auth0Id { get; set; }
    public string InquiryId { get; set; }
   

    public string Name { get; set; }
    public string Email { get; set; }
    public AddressDTO Address { get; set; }
}



public class OfferRespondDTO
{
    public Guid OfferRequestId { get; set; }
    public DateTime ValidTo { get; set; }
}


