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



    public async Task<List<DTOIOD>> GetIODAsync()
    {
        var inquiries = await _inquiryRepository.GetAllInquiriesAsync();
        var offers = await _offerRepository.GetAllByCompany("StachnetCompany");
        var deliveries = await _deliveryRepository.GetAllDeliveriesAsync();

        var offersByInquiryId = offers.ToDictionary(o => o.DeliveryRequestId, o => o);
        var deliveriesByOfferId = deliveries.ToDictionary(d => d.OfferId, d => d);

        var result = new List<DTOIOD>();

        foreach (var inquiry in inquiries)
        {
            var user = await _userRepository.GetBy0IdAsync(inquiry.UserId); 
            UserData UserDTO= null;
            if(user!=null)
            {
                UserDTO = new UserData()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email
                };
            }
                  
                
            
            OfferData? offerDto = null;
            DeliveryData? deliveryDto = null;

            if (offersByInquiryId.TryGetValue(inquiry.DeliveryRequestId, out var offer)) // bezpieczne zwracanie gdy nie ma zwroci false
            {
                offerDto = new OfferData
                {
                    totalPrice = offer.totalPrice,
                    Currency = "PLN" 
                };

                if (deliveriesByOfferId.TryGetValue(offer.OfferId, out var delivery))
                {
                    deliveryDto = new DeliveryData
                    {
                        DeliveryID = delivery.PublicID,
                        PickupDate = delivery.PickupDate,
                        DeliveryDate = delivery.DeliveryDate,
                        DeliveryStatus = delivery.DeliveryStatus,
                        Courier = delivery.Courier
                    };
                }
            }
            ApiAdapter adapter= new ApiAdapter();
            result.Add(new DTOIOD
            {
                
                User = UserDTO,
                Inquiry = new InquiryData
                {
                    InquiryID = inquiry.DeliveryRequestPublicId,
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
            }); 
        }

       

        return result.OrderByDescending(r => r.Inquiry.InquiryDate).ToList();
    }

    
    public async Task<List<DTOIOD>> GetIavailableIODCourierAsync()
    {
        var deliveries = await _deliveryRepository.FindAcceptedDelivery();
        var resultlist= new List<DTOIOD>();

        foreach(var delivery in deliveries) { 
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
            DeliveryStatus = delivery.DeliveryStatus,
            Courier = delivery.Courier
        };

        ApiAdapter adapter = new ApiAdapter();

        DTOIOD result = new DTOIOD()
        {
            User = userData,
            Inquiry = new InquiryData
            {
                InquiryID = inquiry.DeliveryRequestPublicId,
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

    public async Task<DTOIOD> GetIODAsync(Guid DeliveryId)
    {
       

        var delivery = await _deliveryRepository.FindAsync(DeliveryId);

        var offer = delivery.Offer;
        var user = delivery.Offer.DeliveryRequest.User;
        var inquiry = delivery.Offer.DeliveryRequest;
        UserData? userData= null;
        if (user!=null)
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
            DeliveryStatus = delivery.DeliveryStatus,
            Courier = delivery.Courier
        };
            
            ApiAdapter adapter = new ApiAdapter();

        DTOIOD result = new DTOIOD()
        {
            User = userData,
            Inquiry = new InquiryData
            {
                InquiryID = inquiry.DeliveryRequestPublicId,
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

    public async  Task<string> ChangeStatusByWorker(ChangeDeliveryStatusDTO changeDeliveryStatusDTO)
    {

       var delivery=await  _deliveryRepository.FindAsync(changeDeliveryStatusDTO.DeliveryId);

        if(delivery == null) { throw new Exception("Nie znaleziono takiej delivery"); }
      
        delivery.DeliveryStatus = changeDeliveryStatusDTO.DeliveryStatus;
        _deliveryRepository.Update(delivery);
       await _deliveryRepository.SaveChangesAsync();

        return "Udalo sie!";
    }

    public async Task<string> ChangeStatusByCourier(ChangeDeliveryStatusDTO changeDeliveryStatusDTO)
    {

        var delivery = await _deliveryRepository.FindAsync(changeDeliveryStatusDTO.DeliveryId);

        if (delivery == null) { throw new Exception("Nie znaleziono takiej delivery"); }
        DeliveryStatus status = changeDeliveryStatusDTO.DeliveryStatus;

        if (status != DeliveryStatus.AcceptedByKurier||status!=DeliveryStatus.CannotDelivery||status!=DeliveryStatus.PickedUp||status!=DeliveryStatus.Delivered)
            if (delivery == null) { throw new Exception("Nozesz tylko: delivered pickedup accept and Cannotdelivery"); }
        if(status==DeliveryStatus.CannotDelivery&&changeDeliveryStatusDTO.Message==null)
        {
            throw new Exception("Podaj wiadomosc jesli nie udalo sie dostarczyc paczki ( wymagania )");
        }
        if(status==DeliveryStatus.AcceptedByKurier)
        {
           if(changeDeliveryStatusDTO.Auth0Id==null) { throw new Exception("null jako auth0id gdy acceptedbycourier"); }

          User user= await _userRepository.GetByAuth0IdAsync(changeDeliveryStatusDTO.Auth0Id);
            if (user == null) { throw new Exception("Nie ma takiego usera"); }

            delivery.Courier = user;

        }

        delivery.DeliveryStatus = changeDeliveryStatusDTO.DeliveryStatus;
        _deliveryRepository.Update(delivery);
        await _deliveryRepository.SaveChangesAsync();

        return "Udało sie!";
    }


    public async Task<List<DTOIOD>> GetIODCourierMyDeliveryAsync(string Auth0id)
    {
        var deliveries = await _deliveryRepository.FindDeliveriesByCourierId(Auth0id);
        var resultlist = new List<DTOIOD>();

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
                DeliveryStatus = delivery.DeliveryStatus,
                Courier = delivery.Courier
            };

            ApiAdapter adapter = new ApiAdapter();

            DTOIOD result = new DTOIOD()
            {
                User = userData,
                Inquiry = new InquiryData
                {
                    InquiryID = inquiry.DeliveryRequestPublicId,
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
    //public enum DeliveryStatus
    //{
    //    PickedUp,
    //    Delivered,
    //    Failed,
    //    AcceptedByWorker,
    //    AcceptedByKurier,
    //    CancelledByClient,
    //    CancelledByKurier,
    //    CancelledByWorker,
    //    WaitingToAcceptByWorker

    //}

}
