

namespace Domain.Model;

public class Address : BaseEntity
{
    public int AddressId { get; set; }
    public string HouseNumber { get; set; }  
    public string ApartmentNumber { get; set; }  
    public string Street { get; set; }
    public string City { get; set; }
    public string zipCode { get; set; }
    public string Country { get; set; }
}








