namespace Domain.DTO;

public class InquiryDTO
{
    public string UserAuth0 { get; set; }

    public PackageDTO Package { get; set; }
    public AddressDTO SourceAddress { get; set; }
    public AddressDTO DestinationAddress { get; set; }
    public DateTime DeliveryDate { get; set; }
    public bool Priority { get; set; }
    public bool WeekendDelivery { get; set; }
}

public class UserDetailsDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string CompanyName { get; set; }
}

public class AddressDTO
{
    public string HouseNumber { get; set; }
    public string ApartmentNumber { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string zipCode { get; set; }
    public string Country { get; set; }
}

public class PackageDTO
{
    public double Width { get; set; }
    public double Height { get; set; }
    public double Length { get; set; }
    public double Weight { get; set; }
}

/// odebranie inquires od api


public class PriceBreakdown
{
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public string Description { get; set; }
}

public class InquiryToOurApiDTO
{
    public PackageDTO Package { get; set; }
    public AddressDTO SourceAddress { get; set; }
    public AddressDTO DestinationAddress { get; set; }
    public DateTime DeliveryDate { get; set; }
    public bool Priority { get; set; }
    public bool WeekendDelivery { get; set; }
}

public class InquiryToSzymonDTO
{
    public DimensionsDTO Dimensions { get; set; }
    public string Currency { get; set; }
    public double Weight { get; set; }
    public string WeightUnit { get; set; }
    public AddressDTO Source { get; set; }
    public AddressDTO Destination { get; set; }
    public DateTime PickupDate { get; set; }
    public DateTime DeliveryDay { get; set; }
    public bool DeliveryInWeekend { get; set; }
    public string Priority { get; set; }
    public bool VipPackage { get; set; }
    public bool IsCompany { get; set; }
}

public class DimensionsDTO
{
    public double Width { get; set; }
    public double Height { get; set; }
    public double Length { get; set; }
    public string DimensionUnit { get; set; }
}