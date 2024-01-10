using Domain.DTO;
using static System.Net.WebRequestMethods;

namespace Api.Service;




public class OfferService : IOfferService
{
    private readonly HttpClient _httpClientOffer;

    public OfferService(HttpClient httpClient)
    {
        _httpClientOffer = httpClient;
        //_httpClientOffer.BaseAddress= new Uri($"{Configuration["url"]}");
    }

    
    public async Task<DeliveryRespondDTO> GetOffer(DeliveryRequestDTO requestDTO)
    {
        var response = await _httpClientOffer.PostAsJsonAsync("/" requestDTO);
        //response.EnsureSuccessStatusCode();
        //return await response.Content.ReadFromJsonAsync<DeliveryRespondDTO>();
        return response;
    }


}

