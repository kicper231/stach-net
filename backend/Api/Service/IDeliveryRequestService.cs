using Domain.DTO;
using Domain.Model;

namespace Api.Service;

public interface IDeliveryRequestService
{
    List<DeliveryRequest> GetUserDeliveryRequests(string userId);

    Task<List<InquiryRespondDTO>> GetOffers(InquiryDTO deliveryRequest);
    Task<OfferRespondDTO> acceptoffer(OfferDTO offerDTO);
}