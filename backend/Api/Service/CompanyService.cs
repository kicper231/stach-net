using Api.Service;
using Domain.Abstractions;
using Domain.Adapters;
using Domain.DTO;
using Domain.Model;
using Infrastructure;
using Microsoft.AspNetCore.Http.HttpResults;
namespace Api.Service;

public class CompanyService : ICompanyService
{
    private readonly IAddressRepository _addressRepository;
    private readonly ICourierCompanyRepository _courierCompanyRepository;
    private readonly IEmailService _emailService;
    private readonly IPackageRepository _packageRepository;
    private readonly IDeliveryRequestRepository _inquiryRepository;
    private readonly IUserRepository _userRepository;
    private readonly IOfferService _offerService;
    private readonly IOfferRepository _offerRepository;
    private readonly IDeliveryRepository _deliveryRepository;
    private readonly IInquiryServiceFactory _inquiryServiceFactory;


    public CompanyService(IDeliveryRequestRepository repository, IUserRepository repositoryuser,
        IPackageRepository repositorypackage, IAddressRepository repositoryaddress,
        ICourierCompanyRepository courierCompanyRepository, IOfferService offerService, IOfferRepository offerRepository, IInquiryServiceFactory inquiryServiceFactory, IEmailService Iemail, IDeliveryRepository delivery)
    {
        _inquiryRepository = repository;
        _userRepository = repositoryuser;
        _packageRepository = repositorypackage;
        _addressRepository = repositoryaddress;

        _courierCompanyRepository = courierCompanyRepository;
        _offerService = offerService;
        _offerRepository = offerRepository;
        _inquiryServiceFactory = inquiryServiceFactory;
        _emailService = Iemail;
        _deliveryRepository = delivery;
    }



    // dostep do IOD courier i officeworker
    public async Task<DTOIOD> GetIODAsync(Guid DeliveryId)
    {

        ApiAdapter adapter = new ApiAdapter();
        var delivery = await _deliveryRepository.FindAsync(DeliveryId);

        var offer = delivery.Offer;
        var user = delivery.Offer.DeliveryRequest.User;
        var inquiry = delivery.Offer.DeliveryRequest;
        UserData? userData = null;
        if (user != null)
        {
            userData = new UserData()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
        }
        OfferData offerDto = new OfferData
        {
            totalPrice = offer.totalPrice,
            Currency = "PLN"
        };


        DeliveryData deliveryDto = new DeliveryData
        {
            DeliveryID = delivery.PublicID,
            PickupDate = delivery.PickupDate,
            DeliveryDate = delivery.DeliveryDate,
            DeliveryStatus = delivery.DeliveryStatus.ToString(),
            Courier=adapter.ConvertToUserData(delivery.Courier)
        };

       

        DTOIOD result = new DTOIOD()
        {
            User = userData,
            Inquiry = new InquiryData
            {
                InquiryId = inquiry.DeliveryRequestPublicId,
                Package = adapter.ConvertToPackageDTO(inquiry.Package),
                SourceAddress = adapter.ConvertToAddressDTO(inquiry.SourceAddress),
                DestinationAddress = adapter.ConvertToAddressDTO(inquiry.DestinationAddress),
                InquiryDate = inquiry.CreatedAt,
                DeliveryDate = inquiry.DeliveryDate,
                WeekendDelivery = inquiry.WeekendDelivery,
                Priority = inquiry.Priority
            },
            Offer = offerDto,
            Delivery = deliveryDto

        };



        return result;
    }
    // company worker wszystkie IOD
    public async Task<List<InquiryCompanyDTO>> GetCompanyInquries()
    {
        var inquiries = await _inquiryRepository.GetAllInquiriesAsync();
        //var offers = await _offerRepository.GetAllByCompany("StachnetCompany");
        //var deliveries = await _deliveryRepository.GetAllDeliveriesAsync();

        //var offersByInquiryId = offers.ToDictionary(o => o.DeliveryRequestId, o => o);
        //var deliveriesByOfferId = deliveries.ToDictionary(d => d.OfferId, d => d);
        ApiAdapter adapter = new ApiAdapter();
        var result = new List<InquiryCompanyDTO>();

        foreach (var inquiry in inquiries)
        {
            var user = await _userRepository.GetBy0IdAsync(inquiry.UserId); 
            UserData UserDTO= null;
            if (user != null)
            {
                UserDTO = new UserData()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email
                };

            }
            InquiryCompanyDTO Inquiry = new InquiryCompanyDTO()
            {
                InquiryId = inquiry.DeliveryRequestPublicId,
                Package = adapter.ConvertToPackageDTO(inquiry.Package),
                SourceAddress = adapter.ConvertToAddressDTO(inquiry.SourceAddress),
                DestinationAddress = adapter.ConvertToAddressDTO(inquiry.DestinationAddress),
                InquiryDate = inquiry.CreatedAt,
                DeliveryDate = inquiry.DeliveryDate,
                WeekendDelivery = inquiry.WeekendDelivery,
                Priority = inquiry.Priority,
                User = UserDTO
            };
                  
                
            result.Add(Inquiry);
         
        }

       

        return result.OrderByDescending(r => r.InquiryDate).ToList();
    }

    public async Task<List<DeliveryCompanyDTO>> GetCompanyDeliveries()
    {
        List<DeliveryCompanyDTO> result = new List<DeliveryCompanyDTO>();

        var Delivery = await _deliveryRepository.GetAllDeliveriesAsync();
        ApiAdapter adapter = new ApiAdapter();

        foreach (var delivery in Delivery)
        { 
        var offer = delivery.Offer;
        var user = delivery.Offer.DeliveryRequest.User;
        var inquiry = delivery.Offer.DeliveryRequest;
        UserData? userData = null;
        if (user != null)
        {
            userData = new UserData()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
        }
        OfferData offerDto = new OfferData
        {
            totalPrice = offer.totalPrice,
            Currency = "PLN"
        };


            DeliveryData deliveryDto = new DeliveryData
            {
                DeliveryID = delivery.PublicID,
                PickupDate = delivery.PickupDate,
                DeliveryDate = delivery.DeliveryDate,
                DeliveryStatus = delivery.DeliveryStatus.ToString(),
                Courier = adapter.ConvertToUserData(delivery.Courier)
            };
        

        

        DeliveryCompanyDTO item = new DeliveryCompanyDTO()
        {
            User = userData,
            Inquiry = new InquiryData
            {
                InquiryId = inquiry.DeliveryRequestPublicId,
                Package = adapter.ConvertToPackageDTO(inquiry.Package),
                SourceAddress = adapter.ConvertToAddressDTO(inquiry.SourceAddress),
                DestinationAddress = adapter.ConvertToAddressDTO(inquiry.DestinationAddress),
                InquiryDate = inquiry.CreatedAt,
                DeliveryDate = inquiry.DeliveryDate,
                WeekendDelivery = inquiry.WeekendDelivery,
                Priority = inquiry.Priority
            },
            Offer = offerDto,
            Delivery = deliveryDto

        };
            result.Add(item);
        }


        return result.OrderByDescending(r => r.Inquiry.InquiryDate).ToList();
    }
    //zmiana statusu by worker
    public async  Task<string> ChangeStatusByWorker(ChangeDeliveryStatusWorkerDTO changeDeliveryStatusDTO)
    {
        ApiAdapter api = new ApiAdapter();
       var delivery=await  _deliveryRepository.FindAsync(changeDeliveryStatusDTO.DeliveryId);

        if(delivery == null) { throw new Exception("Dont find that delivery"); }
      
        delivery.DeliveryStatus = api.ConvertStringToDeliveryStatus(changeDeliveryStatusDTO.DeliveryStatus);
        _deliveryRepository.Update(delivery);
       await _deliveryRepository.SaveChangesAsync();

        return "OK!";
    }

    // zmiana statusu by worker
    public async Task<string> ChangeStatusByCourier(ChangeDeliveryStatusDTO changeDeliveryStatusDTO)
    {
        ApiAdapter adapter = new ApiAdapter();
        var delivery = await _deliveryRepository.FindAsync(changeDeliveryStatusDTO.DeliveryId);

        if (delivery == null) { throw new Exception("Nie znaleziono takiej delivery"); }
        DeliveryStatus status = adapter.ConvertStringToDeliveryStatus(changeDeliveryStatusDTO.DeliveryStatus);

        if (status != DeliveryStatus.acceptedbycourier||status!=DeliveryStatus.cannotdelivery||status!=DeliveryStatus.pickedup||status!=DeliveryStatus.delivered)
            if (delivery == null) { throw new Exception("Nozesz tylko: delivered pickedup accept and Cannotdelivery"); }
        if(status==DeliveryStatus.cannotdelivery&&changeDeliveryStatusDTO.Message==null)
        {
            throw new Exception("Podaj wiadomosc jesli nie udalo sie dostarczyc paczki ( wymagania )");
        }
        if(status==DeliveryStatus.accepted)
        {
           if(changeDeliveryStatusDTO.Auth0Id==null) { throw new Exception("null jako auth0id gdy acceptedbycourier"); }

          User user= await _userRepository.GetByAuth0IdAsync(changeDeliveryStatusDTO.Auth0Id);
            if (user == null) { throw new Exception("Nie ma takiego usera"); }

            delivery.Courier = user;

        }

        delivery.DeliveryStatus = adapter.ConvertStringToDeliveryStatus(changeDeliveryStatusDTO.DeliveryStatus);
        _deliveryRepository.Update(delivery);
        await _deliveryRepository.SaveChangesAsync();

        return "Udało sie!";
    }

    // Przypisane do danego kouriera IOD
    public async Task<List<DTOIOD>> GetCourierIOD(string Auth0id)
    {
        var deliveries = await _deliveryRepository.FindDeliveriesByCourierId(Auth0id);
        var resultlist = new List<DTOIOD>();
        ApiAdapter adapter = new ApiAdapter();
        foreach (var delivery in deliveries)
        {
            var offer = delivery.Offer;
            var user = delivery.Offer.DeliveryRequest.User;
            var inquiry = delivery.Offer.DeliveryRequest;
            UserData? userData = null;
            if (user != null)
            {
                userData = new UserData()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email
                };
            }
            OfferData offerDto = new OfferData
            {
                totalPrice = offer.totalPrice,
                Currency = "PLN"
            };


            DeliveryData deliveryDto = new DeliveryData
            {
                DeliveryID = delivery.PublicID,
                PickupDate = delivery.PickupDate,
                DeliveryDate = delivery.DeliveryDate,
                DeliveryStatus = delivery.DeliveryStatus.ToString(),
                Courier = adapter.ConvertToUserData(delivery.Courier)
            };

           

            DTOIOD result = new DTOIOD()
            {
                User = userData,
                Inquiry = new InquiryData
                {
                    InquiryId = inquiry.DeliveryRequestPublicId,
                    Package = adapter.ConvertToPackageDTO(inquiry.Package),
                    SourceAddress = adapter.ConvertToAddressDTO(inquiry.SourceAddress),
                    DestinationAddress = adapter.ConvertToAddressDTO(inquiry.DestinationAddress),
                    InquiryDate = inquiry.CreatedAt,
                    DeliveryDate = inquiry.DeliveryDate,
                    WeekendDelivery = inquiry.WeekendDelivery,
                    Priority = inquiry.Priority
                },
                Offer = offerDto,
                Delivery = deliveryDto

            };
            resultlist.Add(result);
        }

        return resultlist;
    }
    // dostepne IOD courier
    public async Task<List<DTOIOD>> GetAvailableIOD()
    {
        var deliveries = await _deliveryRepository.FindAcceptedDelivery();
        var resultlist = new List<DTOIOD>();
        ApiAdapter adapter = new ApiAdapter();

        foreach (var delivery in deliveries)
        {
            var offer = delivery.Offer;
            var user = delivery.Offer.DeliveryRequest.User;
            var inquiry = delivery.Offer.DeliveryRequest;
            UserData? userData = null;
            if (user != null)
            {
                userData = new UserData()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email
                };
            }
            OfferData offerDto = new OfferData
            {
                totalPrice = offer.totalPrice,
                Currency = "PLN"
            };


            DeliveryData deliveryDto = new DeliveryData
            {
                DeliveryID = delivery.PublicID,
                PickupDate = delivery.PickupDate,
                DeliveryDate = delivery.DeliveryDate,
                DeliveryStatus = delivery.DeliveryStatus.ToString(),
                Courier = adapter.ConvertToUserData(delivery.Courier)
            };

           
            DTOIOD result = new DTOIOD()
            {
                User = userData,
                Inquiry = new InquiryData
                {
                    InquiryId = inquiry.DeliveryRequestPublicId,
                    Package = adapter.ConvertToPackageDTO(inquiry.Package),
                    SourceAddress = adapter.ConvertToAddressDTO(inquiry.SourceAddress),
                    DestinationAddress = adapter.ConvertToAddressDTO(inquiry.DestinationAddress),
                    InquiryDate = inquiry.CreatedAt,
                    DeliveryDate = inquiry.DeliveryDate,
                    WeekendDelivery = inquiry.WeekendDelivery,
                    Priority = inquiry.Priority
                },
                Offer = offerDto,
                Delivery = deliveryDto

            };
            resultlist.Add(result);
        }

        return resultlist;
    }


}
