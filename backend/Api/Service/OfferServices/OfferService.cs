using Domain.Adapters;
using Domain.DTO;
using Domain;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text.Json;


namespace Api.Service.OfferServices;
public class OfferService : IOfferService
{
    private readonly HttpClient _httpOurClientOffer;
    private readonly HttpClient _httpSzymonClientOffer;
    private readonly HttpClient _httpTokenSzymonClient;

    private readonly IdentityManagerSettings _settings;

    public OfferService(IHttpClientFactory clientFactory, IOptions<IdentityManagerSettings> settings)
    {
        _httpOurClientOffer = clientFactory.CreateClient("OurClient");
        _httpSzymonClientOffer = clientFactory.CreateClient("SzymonClient");
        _settings = settings.Value;
        _httpTokenSzymonClient = clientFactory.CreateClient("SzymonToken");
    }

    public async Task<string> GetTokenAsync(string companyName)
    {
        var tokenRequest = new HttpRequestMessage(HttpMethod.Post, _settings.TokenEndpointSzymon)
        {
            Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["grant_type"] = "client_credentials",
                ["client_id"] = _settings.ClientIdSzymon,
                ["client_secret"] = _settings.ClientSecretSzymon,
                ["scope"] = _settings.Scope
            })
        };

        var tokenResponse = await _httpTokenSzymonClient.SendAsync(tokenRequest);
        tokenResponse.EnsureSuccessStatusCode();

        var tokenResult = await tokenResponse.Content.ReadAsStringAsync();


        using (var doc = JsonDocument.Parse(tokenResult))
        {
            var accessToken = doc.RootElement.GetProperty("access_token").GetString();
            return accessToken;
        }
    }


    public async Task<OfferRespondDTO> GetOfferOurID(OfferDTO offerdto)
    {

        OfferSzymonApiDTO data = new OfferSzymonApiDTO
        {
            Address = offerdto.Address,
            Name = offerdto.firstName,
            InquiryId = offerdto.InquiryId,
            Email = offerdto.Email

        };

        var response = await _httpOurClientOffer.PostAsJsonAsync("/offers", data);
        var responseBody = await response.Content.ReadAsStringAsync();
        response.EnsureSuccessStatusCode();
        var respond = await response.Content.ReadFromJsonAsync<OfferRespondDTO>();

        return respond;
    }

    public async Task<OfferRespondDTO> GetOfferSzymonID(OfferDTO offerdto)
    {

        OfferSzymonApiDTO data = new OfferSzymonApiDTO
        {
            Address = offerdto.Address,
            Name = offerdto.firstName,
            InquiryId = offerdto.InquiryId,
            Email = offerdto.Email

        };

        var inquirymessage = new HttpRequestMessage(HttpMethod.Post, $"{_httpSzymonClientOffer.BaseAddress}Offers")
        {
            Content = JsonContent.Create(data)
        };


        var accessToken = await GetTokenAsync("SzymonCompany");

        inquirymessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var response = await _httpSzymonClientOffer.SendAsync(inquirymessage);
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();


        var respond = await response.Content.ReadFromJsonAsync<OfferRespondDTO>();



        return respond;
    }


}