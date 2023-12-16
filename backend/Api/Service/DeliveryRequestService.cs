using Domain.Abstractions;
using Domain.DTO;
using Domain.Model;

namespace Api.Service;

public class DeliveryRequestService : IDeliveryRequestService
{
    private readonly IDeliveryRequestRepository _repository;
    private readonly IUserRepository _userRepository;

    public DeliveryRequestService(IDeliveryRequestRepository repository,IUserRepository repositoryuser)
    {
        _repository = repository;
        _userRepository = repositoryuser;
    }

    public List<DeliveryRequest> GetUserDeliveryRequests(string userId)
    {
        return _repository.GetDeliveryRequestsByUserId(userId);
    }

    public void Add(DeliveryRequestDTO deliveryRequestDTO)
    {

        User user = new User
        {
           
            Auth0Id = "twojastaraauth0",
            FirstName = deliveryRequestDTO.UserDetails.FirstName,
            LastName = deliveryRequestDTO.UserDetails.LastName,
            Email = deliveryRequestDTO.UserDetails.Email,
        };

        _userRepository.Add(user);


        var deliveryRequest = new DeliveryRequest
        {
            UserAuth0 = deliveryRequestDTO.UserAuth0,
            DeliveryDate = deliveryRequestDTO.DeliveryDate,
            Status = DeliveryRequestStatus.Pending,

            User = user,


            Package = new Package
            {
                Dimensions = deliveryRequestDTO.Package.Dimensions,
                Weight = deliveryRequestDTO.Package.Weight,
                Priority = deliveryRequestDTO.Package.Priority ? PackagePriority.High : PackagePriority.Low,
                WeekendDelivery = deliveryRequestDTO.Package.WeekendDelivery
            },
            SourceAddress = new Address
            {
                Street = deliveryRequestDTO.SourceAddress.Street,
                City = deliveryRequestDTO.SourceAddress.City,
                PostalCode = deliveryRequestDTO.SourceAddress.PostalCode,
                Country = deliveryRequestDTO.SourceAddress.Country
            },
            DestinationAddress = new Address
            {
                Street = deliveryRequestDTO.DestinationAddress.Street,
                City = deliveryRequestDTO.DestinationAddress.City,
                PostalCode = deliveryRequestDTO.DestinationAddress.PostalCode,
                Country = deliveryRequestDTO.DestinationAddress.Country
            }

            
        };

        _repository.Add(deliveryRequest);
    }




}
