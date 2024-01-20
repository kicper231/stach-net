using Domain.DTO;
using Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace Api.Service;

public interface IClientService
{
    List<UserInquiryDTO> GetUserDeliveryRequests(string userId);

    Task<List<InquiryRespondDTO>> GetOffers(InquiryDTO deliveryRequest);
    Task<OfferRespondDTO> acceptoffer(OfferDTO offerDTO);

    bool UserExists(string idAuth0);
    Task<AddDeliveryRespondDTO> AddDeliveryToAccount(AddDeliveryDTO add);

    Task<string> CancelDelivery(CancelDeliveryDTO cancelDeliveryDTO);
}