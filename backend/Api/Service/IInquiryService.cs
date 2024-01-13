using Domain.DTO;

namespace Api.Service;

public interface IInquiryService
{
    public Task<InquiryRespondDTO> GetOffersFromOurApi(InquiryDTO requestDTO);

    public Task<InquiryRespondDTO> GetOfferFromSzymonApi(InquiryDTO InquiryDTO);
    public Task<string> GetTokenAsync();
} 