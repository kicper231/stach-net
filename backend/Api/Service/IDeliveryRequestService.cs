using Domain.DTO;
using Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace Api.Service
{
    public interface IDeliveryRequestService
    {
        List<DeliveryRequest> GetUserDeliveryRequests(string userId);
        Task<DeliveryRespondDTO> Add(DeliveryRequestDTO deliveryRequest);


    }
}
