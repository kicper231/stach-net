using Domain.Model;

namespace Api.Service
{
    public interface IDeliveryRequestService
    {
        List<DeliveryRequest> GetUserDeliveryRequests(string userId);
      
    }
}
