using Domain.DTO;
using Domain;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Net.Http.Headers;
using Domain.Adapters;

namespace Api.Service;



public class KamilInquiryService : IInquiryService
{
    private readonly HttpClient _httpClient;

    private readonly IdentityManagerSettings _settings;
    private readonly IApiAdapter _ApiAdapter;

    public KamilInquiryService(IHttpClientFactory clientFactory, IOptions<IdentityManagerSettings> settings,IApiAdapter adapter)
    {
        _httpClient = clientFactory.CreateClient("KamilClient");
        _settings = settings.Value;
        _ApiAdapter = adapter;
    }

    public async Task<string?> GetTokenAsync()
    {
        return default;
    }

    public async Task<InquiryRespondDTO> GetOffers(InquiryDTO requestDTO)
    {

        
        var inquiryToSzymonDTO = _ApiAdapter.ConvertToInquiryToSzymonDTO(requestDTO);

        var inquirymessage = new HttpRequestMessage(HttpMethod.Post, $"{_httpClient.BaseAddress}Inquires")
        {
            Content = JsonContent.Create(inquiryToSzymonDTO)
        };


        var accessToken = await GetTokenAsync();

        inquirymessage.Headers.Authorization = new AuthenticationHeaderValue("ApiKey", accessToken);

        var response = await _httpClient.SendAsync(inquirymessage);
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();

        var respond = await response.Content.ReadFromJsonAsync<InquiryRespondDTO>();
        respond.CompanyName = "KamilCompany";


        return respond;
    }
}
