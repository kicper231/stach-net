using Domain.DTO;
using Domain;
using Microsoft.Extensions.Options;
using Domain.Adapters;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Api.Service;


public class SzymonInquiryService : IInquiryService
{
    private readonly HttpClient _httpClient;
    private readonly IdentityManagerSettings _settings;
    private readonly HttpClient _httpClientToken;
    public SzymonInquiryService(IHttpClientFactory clientFactory, IOptions<IdentityManagerSettings> settings)
    {
        _httpClient = clientFactory.CreateClient("SzymonClient");
        _settings = settings.Value;
        _httpClientToken = clientFactory.CreateClient("SzymonToken");
    }

    public async Task<string?> GetTokenAsync()
    {
        HttpRequestMessage? tokenRequest = null;



        tokenRequest = new HttpRequestMessage(HttpMethod.Post, _settings.TokenEndpointSzymon)
        {
            Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["grant_type"] = "client_credentials",
                ["client_id"] = _settings.ClientIdSzymon,
                ["client_secret"] = _settings.ClientSecretSzymon,
                ["scope"] = _settings.Scope
            })
        };



        var tokenResponse = await _httpClientToken.SendAsync(tokenRequest);
        tokenResponse.EnsureSuccessStatusCode();

        var tokenResult = await tokenResponse.Content.ReadAsStringAsync();


        using (var doc = JsonDocument.Parse(tokenResult))
        {
            var accessToken = doc.RootElement.GetProperty("access_token").GetString();
            return accessToken;
        }
    }

    public async Task<InquiryRespondDTO> GetOffers(InquiryDTO requestDTO)
    {
        var ApiAdapter = new ApiAdapter();
        var inquiryToSzymonDTO = ApiAdapter.ConvertToInquiryToSzymonDTO(requestDTO);

        var inquirymessage = new HttpRequestMessage(HttpMethod.Post, $"{_httpClient.BaseAddress}Inquires")
        {
            Content = JsonContent.Create(inquiryToSzymonDTO)
        };


        var accessToken = await GetTokenAsync();

        inquirymessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var response = await _httpClient.SendAsync(inquirymessage);
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();
        
        var respond = await response.Content.ReadFromJsonAsync<InquiryRespondDTO>();
        respond.CompanyName = "SzymonCompany";


        return respond;
    }
}
