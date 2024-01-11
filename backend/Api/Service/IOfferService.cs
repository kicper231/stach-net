using Domain.DTO;

namespace Api.Service
{
    public interface IOfferService
    {
         
        Task<DeliveryRespondDTO> GetOffers(DeliveryRequestDTO requestDTO);
    }
}
