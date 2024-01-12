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

    public async Task<List<DeliveryRespondDTO?>> GetOffers(InquiryDTO deliveryRequestDTO)
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
            zipCode = deliveryRequestDTO.SourceAddress.zipCode,
            Country = deliveryRequestDTO.SourceAddress.Country
        };
        _addressRepository.Add(sourceAddress);


        var destinationAddress = new Address
        {
            ApartmentNumber = deliveryRequestDTO.DestinationAddress.ApartmentNumber,
            HouseNumber = deliveryRequestDTO.DestinationAddress.HouseNumber,
            Street = deliveryRequestDTO.DestinationAddress.Street,
            City = deliveryRequestDTO.DestinationAddress.City,
            zipCode = deliveryRequestDTO.DestinationAddress.zipCode,
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


        //DeliveryRespondDTO respondfromourapi;
        //DeliveryRespondDTO respondfromourapi1;
        List<DeliveryRespondDTO?> offersToSend = new List<DeliveryRespondDTO?>();
        // DeliveryRespondDTO deliveryRespondDTO = await _httpOffersServise.GetOffersFromOurApi(deliveryRequestDTO);

        //Parallel.Invoke(async () =>
        //{ respondfromourapi = await _httpOffersServise.GetOffersFromOurApi(deliveryRequestDTO); offersToSend.Add(respondfromourapi); },
        //  async () => { respondfromourapi1 = await _httpOffersServise.GetOffersFromOurApi(deliveryRequestDTO); offersToSend.Add(respondfromourapi1); }
        //   );



        var tasks = new List<Task<DeliveryRespondDTO?>>();


        tasks.Add(SafeGetOffer(_httpOffersServise.GetOfferFromSzymonApi(deliveryRequestDTO)));
        tasks.Add(SafeGetOffer(_httpOffersServise.GetOffersFromOurApi(deliveryRequestDTO)));

        var responseparrarel= await Task.WhenAll(tasks);

        offersToSend.AddRange(responseparrarel);


        offersToSend.Add(new DeliveryRespondDTO
        {
            CompanyName = "Company C",
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

       
        return   offersToSend;
           



       
    }


    private async Task<DeliveryRespondDTO?> SafeGetOffer(Task<DeliveryRespondDTO> task)
    {
        try
        {
            return await task;
        }
        catch (HttpRequestException e)
        {
           Console.WriteLine($"Błąd podczas wykonywania żądania: {e.Message} {e.Data}");
            return null;
        }
    }


}
