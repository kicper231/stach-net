using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Domain.DTO
{
    public class DeliveryCompanyDTO
    {
        public UserData? User { get; set; }
        public required InquiryData Inquiry { get; set; }
        public required OfferData Offer { get; set; }
        public required DeliveryData Delivery { get; set; }
    }



   


}