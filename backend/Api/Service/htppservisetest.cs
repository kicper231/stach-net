using Domain.DTO;

namespace Api.Service;



public interface IOfferService
{
   // Task<string> GetAsync(string url);  
     Task<DeliveryRespondDTO> GetOffer(string url, DeliveryRequestDTO requestDTO);
}
public class OfferService : IOfferService
{
    private readonly HttpClient _httpClient;

    public OfferService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        //_httpClient.BaseAddress=
        // do zmiany //secret key 
    }

    //public async Task<string> GetAsync(string url)
    //{
    //    var response = await _httpClient.GetAsync(url);
    //    response.EnsureSuccessStatusCode();
    //    return await response.Content.ReadAsStringAsync();
    //}

    public async Task<DeliveryRespondDTO> GetOffer(string url, DeliveryRequestDTO requestDTO)
    {
        var response = await _httpClient.PostAsJsonAsync(url, requestDTO);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<DeliveryRespondDTO>();
    }


}

