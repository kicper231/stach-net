using Domain.DTO;

namespace Api.Service;
public interface IInquiryService
{


    public Task<InquiryRespondDTO> GetOffers(InquiryDTO requestDTO);
    public  Task<string?> GetTokenAsync();
}