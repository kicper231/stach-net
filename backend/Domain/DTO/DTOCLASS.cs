using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO;

public class DeliveryRequestDTO
{
    public string UserAuth0 { get; set; }
   
    public UserDetailsDTO UserDetails { get; set; }
    public PackageDTO Package { get; set; }
    public AddressDTO SourceAddress { get; set; }
    public AddressDTO DestinationAddress { get; set; }
    public DateTime DeliveryDate { get; set; }
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
    public string Street { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
}

public class PackageDTO
{
    public string Dimensions { get; set; }
    public double Weight { get; set; }
    public bool Priority { get; set; }
    public bool WeekendDelivery { get; set; }
}

