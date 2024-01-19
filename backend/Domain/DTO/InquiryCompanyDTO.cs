using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO;




public class InquiryCompanyDTO
{



    public UserData? User { get; set; }

    public Guid InquiryID { get; set; }
    public required PackageDTO Package { get; set; }

    public required AddressDTO SourceAddress { get; set; }

    public required AddressDTO DestinationAddress { get; set; }
    public DateTime InquiryDate { get; set; }
    public DateTime DeliveryDate { get; set; }

    public bool WeekendDelivery { get; set; }
    public PackagePriority Priority { get; set; }



}



