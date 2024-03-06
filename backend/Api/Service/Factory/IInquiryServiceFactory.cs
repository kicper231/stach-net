
using Domain;
using Domain.Adapters;
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
    private readonly IApiAdapter _apiAdapter;

    public InquiryServiceFactory(IHttpClientFactory clientFactory, IOptions<IdentityManagerSettings> settings,IApiAdapter apiAdapter)

    {
        _clientFactory = clientFactory;
        _settings = settings;
        _apiAdapter = apiAdapter;
    }

    public IInquiryService CreateService(string companyName)
    {
        switch (companyName)
        {
            case "StachnetCompany":
                return new StachnetInquiryService(_clientFactory, _settings);
            case "SzymonCompany":
                return new SzymonInquiryService(_clientFactory, _settings, _apiAdapter);
            case "KamilCompany":
                return new KamilInquiryService(_clientFactory, _settings,_apiAdapter);
            default:
                throw new ArgumentException("Nieznana nazwa firmy", nameof(companyName));
            
        }
    }
}