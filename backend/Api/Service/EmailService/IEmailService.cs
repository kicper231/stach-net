using Domain.DTO;

namespace Api.Service;

public interface IEmailService
{
    public Task<bool> AfterInquiry(InquiryDTO inquiryDTO, string name, string email);
}
