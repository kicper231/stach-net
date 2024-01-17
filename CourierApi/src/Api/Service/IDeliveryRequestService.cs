using Domain.DTO;
using Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace Api.Service
{
    public interface IDeliveryRequest
    {
        //List<DeliveryRequestDTO> GetUserDeliveryRequests(string userId);
        OfferRespondDTO AcceptOffer(OfferDTO offer);
        InquiryDTO GetOffers(DeliveryRequestDTO deliveryRequest);
        

    }
    public class Inquiries : IDeliveryRequest
    {

       public InquiryDTO GetOffers(DeliveryRequestDTO deliveryRequest)
        {
            return new InquiryDTO
            {
                InquiryId = "123abc",
                TotalPrice = 150.99,
                Currency = "USD",
                InquiryDTOGuid = Guid.NewGuid(),
                ExpiringAt = DateTime.Now.AddDays(7),
                PriceBreakDown = new List<PriceBreakdown>
    {
        new PriceBreakdown { Amount = 80, Currency = "PLN", Description = "Podstawowa cena" },
        new PriceBreakdown { Amount = 10, Currency = "PLN", Description = "Podatek VAT" },
        new PriceBreakdown { Amount = 5, Currency = "PLN", Description = "Opłata za dostawę" },
    }
            };



        }

        


        public OfferRespondDTO AcceptOffer(OfferDTO offer)
        {
            return new OfferRespondDTO { OfferRequestId=offer.OfferDTOGuid, ValidTo = DateAndTime.Now };
        }
    }

}
