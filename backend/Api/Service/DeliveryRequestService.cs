using Domain.Abstractions;
using Domain.DTO;
using Domain.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Api.Service;

public class DeliveryRequestService : IDeliveryRequestService
{
    private readonly IDeliveryRequestRepository _repository;
    private readonly IUserRepository _userRepository;
    private readonly IPackageRepository _packageRepository;
    private readonly IAddressRepository _addressRepository;
    private readonly IOfferService _httpService;

    public DeliveryRequestService(IDeliveryRequestRepository repository,IUserRepository repositoryuser,IPackageRepository repositorypackage,IAddressRepository repositoryaddress, IOfferService httpService)
    {
        _repository = repository;
        _userRepository = repositoryuser;
        _packageRepository = repositorypackage;
        _addressRepository = repositoryaddress;
        _httpService = httpService;
    }

    public List<DeliveryRequest> GetUserDeliveryRequests(string userId)
    {
        return _repository.GetDeliveryRequestsByUserId(userId);
    }

    public async Task<List<DeliveryRespondDTO>> GetOffers(DeliveryRequestDTO deliveryRequestDTO)
    {



        //package 
        var package = new Package
        {
            Height = deliveryRequestDTO.Package.Height,
            Width = deliveryRequestDTO.Package.Width,
            Length = deliveryRequestDTO.Package.Length,

            Weight = deliveryRequestDTO.Package.Weight,
         
        };
         _packageRepository.Add(package);

        // adress
        var sourceAddress = new Address
        {
            ApartmentNumber = deliveryRequestDTO.SourceAddress.ApartmentNumber,
            HouseNumber = deliveryRequestDTO.SourceAddress.HouseNumber,
            Street = deliveryRequestDTO.SourceAddress.Street,
            City = deliveryRequestDTO.SourceAddress.City,
            PostalCode = deliveryRequestDTO.SourceAddress.PostalCode,
            Country = deliveryRequestDTO.SourceAddress.Country
        };
         _addressRepository.Add(sourceAddress); 

       
        var destinationAddress = new Address
        {
            ApartmentNumber=deliveryRequestDTO.DestinationAddress.ApartmentNumber,
            HouseNumber = deliveryRequestDTO.DestinationAddress.HouseNumber,
            Street = deliveryRequestDTO.DestinationAddress.Street,
            City = deliveryRequestDTO.DestinationAddress.City,
            PostalCode = deliveryRequestDTO.DestinationAddress.PostalCode,
            Country = deliveryRequestDTO.DestinationAddress.Country
        };
         _addressRepository.Add(destinationAddress);

        // create deliveryrequest with reference (it will work??) 
        var deliveryRequest = new DeliveryRequest
        {
           // UserAuth0 = deliveryRequestDTO.UserAuth0,
           // User = null,
            DeliveryDate = deliveryRequestDTO.DeliveryDate,
            Status = DeliveryRequestStatus.Pending,

            Package = package,
            SourceAddress = sourceAddress,
            DestinationAddress = destinationAddress,
            Priority = deliveryRequestDTO.Priority ? PackagePriority.High : PackagePriority.Low,
            WeekendDelivery = deliveryRequestDTO.WeekendDelivery
        };

        _repository.Add(deliveryRequest);


        //string externalApiUrl = "https://localhost:7286/WeatherForecast";
        //var deliveryRespondDTO = await _httpService.GetOffer(externalApiUrl, deliveryRequestDTO);


        List<DeliveryRespondDTO> offers = new List<DeliveryRespondDTO>();

       
        offers.Add(new DeliveryRespondDTO
        {
            CompanyName = "Company A",
            Cost = 100,  // Example cost
            DeliveryDate = DateTime.Now.AddDays(5)  
        });

        offers.Add(new DeliveryRespondDTO
        {
            CompanyName = "Company B",
            Cost = 120,  // Example cost
            DeliveryDate = DateTime.Now.AddDays(4) 
        });

        offers.Add(new DeliveryRespondDTO
        {
            CompanyName = "Company C",
            Cost = 90,  // Example cost
            DeliveryDate = DateTime.Now.AddDays(6)  // Example delivery date
        });


        return offers;
    }





}
