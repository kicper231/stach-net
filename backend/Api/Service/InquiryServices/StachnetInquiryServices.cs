using Domain;
using Domain.DTO;
using Microsoft.Extensions.Options;

namespace Api.Service;

public class StachnetInquiryService : IInquiryService
{
    private readonly HttpClient _httpClient;

    private readonly IdentityManagerSettings _settings;

    public StachnetInquiryService(IHttpClientFactory clientFactory, IOptions<IdentityManagerSettings> settings)
    {
        _httpClient = clientFactory.CreateClient("OurClient");
        _settings = settings.Value;
    }

    public async Task<string?> GetTokenAsync()
    {
        return default;
    }

    public async Task<InquiryRespondDTO> GetOffers(InquiryDTO requestDTO)
    {
        var response = await _httpClient.PostAsJsonAsync("/inquiries", requestDTO);
        var responseBody = await response.Content.ReadAsStringAsync();
        response.EnsureSuccessStatusCode();

        var respond = await response.Content.ReadFromJsonAsync<InquiryRespondDTO>();
        respond.CompanyName = "StachnetCompany";
        return respond;
    }
}