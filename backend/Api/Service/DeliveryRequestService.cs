using Domain.Abstractions;
using Domain.Model;

namespace Api.Service;

public class DeliveryRequestService : IDeliveryRequestService
{
    private readonly IDeliveryRequestRepository _repository;

    public DeliveryRequestService(IDeliveryRequestRepository repository)
    {
        _repository = repository;
    }

    public List<DeliveryRequest> GetUserDeliveryRequests(string userId)
    {
        return _repository.GetDeliveryRequestsByUserId(userId);
    }

    
}
