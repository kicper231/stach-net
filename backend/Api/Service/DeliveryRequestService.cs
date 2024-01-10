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
    private readonly IOfferService _httpOffersServise;

    public DeliveryRequestService(IDeliveryRequestRepository repository,IUserRepository repositoryuser,IPackageRepository repositorypackage,IAddressRepository repositoryaddress, IOfferService httpService)
    {
        _repository = repository;
        _userRepository = repositoryuser;
        _packageRepository = repositorypackage;
        _addressRepository = repositoryaddress;
        _httpOffersServise = httpService;
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
        deliveryRequest.CreatedAt = DateTime.Now;
       _repository.Add(deliveryRequest);


       
        DeliveryRespondDTO deliveryRespondDTO = await _httpOffersServise.GetOffers(deliveryRequestDTO);


        List<DeliveryRespondDTO> offers = new List<DeliveryRespondDTO>();



        List<DeliveryRespondDTO> offersToSend = new List<DeliveryRespondDTO>();


        offersToSend.Add(deliveryRespondDTO);

    
        offersToSend.Add(new DeliveryRespondDTO
        {
            CompanyName = "Company B",
            Cost = 120, 
            DeliveryDate = DateTime.Now.AddDays(4),
            InquiryId = "SomeInquiryId2",
            PriceBreakDown = new List<PriceBreakdown>
    {
        new PriceBreakdown { Amount = 90, Currency = "PLN", Description = "Podstawowa cena" },
        new PriceBreakdown { Amount = 15, Currency = "PLN", Description = "Podatek VAT" },
        new PriceBreakdown { Amount = 10, Currency = "PLN", Description = "Opłata za dostawę" },
        
    }
        });

       
        return offersToSend;




       
    }





}
