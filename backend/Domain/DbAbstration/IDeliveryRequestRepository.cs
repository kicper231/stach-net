using Domain.Model;

namespace Domain.Abstractions;

public interface IDeliveryRequestRepository
{
    List<DeliveryRequest> GetDeliveryRequestsByUserId(string userId);
    public void Add(DeliveryRequest delivery);


}

