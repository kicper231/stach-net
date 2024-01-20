using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO;





public class UserInquiryDTO
{
    public PackageDTO Package { get; set; }
    public AddressDTO SourceAddress { get; set; }
    public AddressDTO DestinationAddress { get; set; }
    public DateTime DeliveryDate { get; set; }
   
    public DateTime CreatedTime { get; set; }
    public bool WeekendDelivery { get; set; }
    public PackagePriority Priority { get; set; }

    public UserDeliveryDTO? DeliveryInfo { get; set; }
  
}


public class UserDeliveryDTO
{
    public string Currency { get; set; }
    public double totalPrice { get; set; }
    public Guid DeliveryId { get; set; }
    public string? DeliveryStatus { get; set; }
}