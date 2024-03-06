using Domain.DTO;

namespace Api.Service;

public interface IEmailService
{
    public Task<bool> AfterInquiry(InquiryDTO inquiryDTO, string name, string email);
    public Task<bool> DeliveryCreate(Guid guid, string name, string email);
}
