using Domain.DTO;

namespace Api.Service;

public interface IOfferService
{
    public Task<DeliveryRespondDTO> GetOffersFromOurApi(InquiryDTO requestDTO);

    public Task<DeliveryRespondDTO> GetOfferFromSzymonApi(InquiryDTO InquiryDTO);
    public Task<string> GetTokenAsync();
}