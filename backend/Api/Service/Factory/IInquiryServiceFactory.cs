using Domain;
using Microsoft.Extensions.Options;

namespace Api.Service;

public interface IInquiryServiceFactory
{
    IInquiryService CreateService(string companyName);
}

public class InquiryServiceFactory : IInquiryServiceFactory
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly IOptions<IdentityManagerSettings> _settings;

    public InquiryServiceFactory(IHttpClientFactory clientFactory, IOptions<IdentityManagerSettings> settings)
    {
        _clientFactory = clientFactory;
        _settings = settings;
    }

    public IInquiryService CreateService(string companyName)
    {
        switch (companyName)
        {
            case "StachnetCompany":
                return new StachnetInquiryService(_clientFactory, _settings);

            case "SzymonCompany":
                return new SzymonInquiryService(_clientFactory, _settings);

            default:
                throw new ArgumentException("Nieznana nazwa firmy", nameof(companyName));
        }
    }
}