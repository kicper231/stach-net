using Domain.DTO;

namespace Api.Service;



public interface IHttpService
{
    Task<string> GetAsync(string url);
     Task<DeliveryRespondDTO> PostDeliveryRequestAsync(string url, DeliveryRequestDTO requestDTO);
}
public class HttpService : IHttpService
{
    private readonly HttpClient _httpClient;

    public HttpService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetAsync(string url)
    {
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<DeliveryRespondDTO> PostDeliveryRequestAsync(string url, DeliveryRequestDTO requestDTO)
    {
        var response = await _httpClient.PostAsJsonAsync(url, requestDTO);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<DeliveryRespondDTO>();
    }


}

