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
}
