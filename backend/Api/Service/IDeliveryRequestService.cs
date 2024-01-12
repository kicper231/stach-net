using Domain.DTO;
using Domain.Model;

namespace Api.Service
{
    public interface IDeliveryRequestService
    {
        List<DeliveryRequest> GetUserDeliveryRequests(string userId);

        Task<List<DeliveryRespondDTO>> GetOffers(InquiryDTO deliveryRequest);

    }
}
