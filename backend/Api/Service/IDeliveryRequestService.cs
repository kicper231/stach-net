using Domain.DTO;
using Domain.Model;

namespace Api.Service
{
    public interface IDeliveryRequestService
    {
        List<DeliveryRequest> GetUserDeliveryRequests(string userId);
        void Add(DeliveryRequestDTO deliveryRequest);


    }
}
