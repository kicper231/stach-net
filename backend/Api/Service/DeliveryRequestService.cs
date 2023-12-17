using Domain.Abstractions;
using Domain.DTO;
using Domain.Model;

namespace Api.Service;

public class DeliveryRequestService : IDeliveryRequestService
{
    private readonly IDeliveryRequestRepository _repository;
    private readonly IUserRepository _userRepository;
    private readonly IPackageRepository _packageRepository;
    private readonly IAddressRepository _addressRepository;

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
        // create user example before actions or claim from token (hamsko)
        User user = new User
        {
            Auth0Id = "twojastaraauth0",
            FirstName = deliveryRequestDTO.UserDetails.FirstName,
            LastName = deliveryRequestDTO.UserDetails.LastName,
            Email = deliveryRequestDTO.UserDetails.Email,
        };
        _userRepository.Add(user);

        //package 
        var package = new Package
        {
            Dimensions = deliveryRequestDTO.Package.Dimensions,
            Weight = deliveryRequestDTO.Package.Weight,
            Priority = deliveryRequestDTO.Package.Priority ? PackagePriority.High : PackagePriority.Low,
            WeekendDelivery = deliveryRequestDTO.Package.WeekendDelivery
        };
         _packageRepository.Add(package); 

        // adress
        var sourceAddress = new Address
        {
            Street = deliveryRequestDTO.SourceAddress.Street,
            City = deliveryRequestDTO.SourceAddress.City,
            PostalCode = deliveryRequestDTO.SourceAddress.PostalCode,
            Country = deliveryRequestDTO.SourceAddress.Country
        };
         _addressRepository.Add(sourceAddress); 

       
        var destinationAddress = new Address
        {
            Street = deliveryRequestDTO.DestinationAddress.Street,
            City = deliveryRequestDTO.DestinationAddress.City,
            PostalCode = deliveryRequestDTO.DestinationAddress.PostalCode,
            Country = deliveryRequestDTO.DestinationAddress.Country
        };
         _addressRepository.Add(destinationAddress); 

        // create deliveryrequest with reference (it will work??) 
        var deliveryRequest = new DeliveryRequest
        {
            UserAuth0 = deliveryRequestDTO.UserAuth0,
            DeliveryDate = deliveryRequestDTO.DeliveryDate,
            Status = DeliveryRequestStatus.Pending,
            User = user,
            Package = package,
            SourceAddress = sourceAddress,
            DestinationAddress = destinationAddress
        };

        _repository.Add(deliveryRequest);
    }





}
