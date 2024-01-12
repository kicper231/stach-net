using Domain;
using Domain.Adapters;
using Domain.DTO;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text.Json;

namespace Api.Service;

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

    public async Task<string> GetTokenAsync()
    {
        var tokenRequest = new HttpRequestMessage(HttpMethod.Post, _settings.TokenEndpoint)
        {
            Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["grant_type"] = "client_credentials",
                ["client_id"] = _settings.ClientId,
                ["client_secret"] = _settings.ClientSecret,
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

    public async Task<DeliveryRespondDTO> GetOffersFromOurApi(InquiryDTO requestDTO)
    {
        var response = await _httpOurClientOffer.PostAsJsonAsync("/inquiries", requestDTO);
        var responseBody = await response.Content.ReadAsStringAsync();
        response.EnsureSuccessStatusCode();
        // var token = await GetTokenAsync();
        // Console.WriteLine(token);
        //return await response.Content.ReadFromJsonAsync<DeliveryRespondDTO>();
        var respond = await response.Content.ReadFromJsonAsync<DeliveryRespondDTO>();
        respond.CompanyName = "StachnetCompany";
        return respond;
    }


    public async Task<DeliveryRespondDTO> GetOfferFromSzymonApi(InquiryDTO requestDTO)
    {
        var szymonApiAdapter = new SzymonApiAdapter();
        var inquiryToSzymonDTO = szymonApiAdapter.ConvertToInquiryToSzymonDTO(requestDTO);

        var inquirymessage = new HttpRequestMessage(HttpMethod.Post, $"{_httpSzymonClientOffer.BaseAddress}Inquires")
        {
            Content = JsonContent.Create(inquiryToSzymonDTO)
        };


        var accessToken = await GetTokenAsync();

        inquirymessage.Headers.Authorization =new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

        var response = await _httpSzymonClientOffer.SendAsync(inquirymessage);
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();
        //if (!response.IsSuccessStatusCode)
        //{
        //    Log.Error($"Błąd! Status odpowiedzi: {response.StatusCode}");
        //    Log.Error($"Treść odpowiedzi: {response.Content}");
        //    // Możesz tutaj obsłużyć błąd, zalogować go, rzucić wyjątek, itp.
        //}


        //if (!response.IsSuccessStatusCode)
        //{
        //    // Unwrap the response and throw as an Api Exception:
        //    var ex = CreateExceptionFromResponseErrors(response);
        //    throw ex;
        //}


        var respond = await response.Content.ReadFromJsonAsync<DeliveryRespondDTO>();
        respond.CompanyName = "SzymonCompany";


        return respond;
    }


    public static Exception CreateExceptionFromResponseErrors(HttpResponseMessage response)
    {
        var httpErrorObject = response.Content.ReadAsStringAsync().Result;

        // Create an anonymous object to use as the template for deserialization:
        var anonymousErrorObject =
            new { message = "", ModelState = new Dictionary<string, string[]>() };

        // Deserialize:
        var deserializedErrorObject =
            JsonConvert.DeserializeAnonymousType(httpErrorObject, anonymousErrorObject);

        // Now wrap into an exception which best fullfills the needs of your application:
        var ex = new Exception();

        // Sometimes, there may be Model Errors:
        if (deserializedErrorObject.ModelState != null)
        {
            var errors =
                deserializedErrorObject.ModelState
                    .Select(kvp => string.Join(". ", kvp.Value));
            for (var i = 0; i < errors.Count(); i++)
                // Wrap the errors up into the base Exception.Data Dictionary:
                ex.Data.Add(i, errors.ElementAt(i));
        }
        // Othertimes, there may not be Model Errors:
        else
        {
            var error =
                JsonConvert.DeserializeObject<Dictionary<string, string>>(httpErrorObject);
            foreach (var kvp in error)
                // Wrap the errors up into the base Exception.Data Dictionary:
                ex.Data.Add(kvp.Key, kvp.Value);
        }

        return ex;
    }
}