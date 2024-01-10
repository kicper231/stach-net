using Domain.DTO;
using Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace Api.Service
{
    public interface IInquiries
    {
        //List<DeliveryRequestDTO> GetUserDeliveryRequests(string userId);

        InquiryDTO GetOffers(DeliveryRequestDTO deliveryRequest);

    }
    public class Inquiries : IInquiries
    {

       public InquiryDTO GetOffers(DeliveryRequestDTO deliveryRequest)
        {
            return new InquiryDTO
            {
                InquiryId = "123abc",
                TotalPrice = 150.99m,
                Currency = "USD",
                ExpiringAt = DateTime.Now.AddDays(7),
                PriceBreakDown = new List<PriceBreakdown>
    {
        new PriceBreakdown { Amount = 80, Currency = "PLN", Description = "Podstawowa cena" },
        new PriceBreakdown { Amount = 10, Currency = "PLN", Description = "Podatek VAT" },
        new PriceBreakdown { Amount = 5, Currency = "PLN", Description = "Opłata za dostawę" },
    }
            };



        }


    }

}
