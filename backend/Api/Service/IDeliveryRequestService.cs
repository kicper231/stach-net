using Domain.DTO;
using Domain.Model;

namespace Api.Service;

public interface IDeliveryRequestService
{
    List<UserInquiryDTO> GetUserDeliveryRequests(string userId);

    Task<List<InquiryRespondDTO>> GetOffers(InquiryDTO deliveryRequest);
    Task<OfferRespondDTO> acceptoffer(OfferDTO offerDTO);

    bool UserExists(string idAuth0);
}