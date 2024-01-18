using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class DTOIOD
    {
        public UserData? User { get; set; }
        public required InquiryData  Inquiry { get; set; }
        public OfferData? Offer { get; set; }
        public DeliveryData? Delivery { get; set; }
    }



    public class InquiryData
    {



        //  public UserDTO? User { get; set; }

        public Guid InquiryID { get; set; }
        public required PackageDTO Package { get; set; }

        public required AddressDTO SourceAddress { get; set; }

        public required  AddressDTO DestinationAddress { get; set; }
        public DateTime InquiryDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        
        public bool WeekendDelivery { get; set; }
        public PackagePriority Priority { get; set; }

    

    }

    public class OfferData
    {
      
    
   
      


        public double totalPrice { get; set; }
        public string Currency { get; set; }
      //  public string CompanyName { get; set; }

    }

    public class DeliveryData
    {
        // Właściwości związane z dostawą
      
        public Guid DeliveryID { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }
        public User? Courier { get; set; }

        // Dodaj inne właściwości związane z dostawą
    }

    public class UserData
    {


        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
    }


}
