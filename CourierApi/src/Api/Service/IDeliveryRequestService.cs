using Domain.Abstractions;
using Domain.DTO;
using Domain.Model;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace Api.Service
{
    public interface IDeliveryRequest
    {
        OfferRespondDTO AcceptOffer(OfferDTO offer);
        InquiryDTO GetOffers(DeliveryRequestDTO deliveryRequest);
        public string Validate(DeliveryRequestDTO DRDTO);
        public void SaveInDatabaseDelivery(OfferDTO DRDTO, OfferRespondDTO respond);
        public void SaveInDatabaseDeliveryRequest(DeliveryRequestDTO DRDTO, InquiryDTO response);
        public ShopperContext GetDeliveryContext();
        public Task<Delivery> Find(Guid g);
        public void DatabaseSave();

    }
    public class Inquiries : IDeliveryRequest
    {
        private readonly IAddressRepository _addressRepository;

        private readonly IPackageRepository _packageRepository;
        private readonly IDeliveryRequestRepository _inquiryRepository;
        private readonly IUserRepository _userRepository;
        private readonly IDeliveryRepository _deliveryRepository;
        public Inquiries(IDeliveryRequestRepository repository, IUserRepository repositoryuser,
        IPackageRepository repositorypackage, IAddressRepository repositoryaddress,
        IDeliveryRepository delivery)
        {
            _inquiryRepository = repository;
            _userRepository = repositoryuser;
            _packageRepository = repositorypackage;
            _addressRepository = repositoryaddress;
            _deliveryRepository = delivery;
        }
        public ShopperContext GetDeliveryContext()
        {
            return _deliveryRepository.GetContext();
        }
        public InquiryDTO GetOffers(DeliveryRequestDTO deliveryRequest)
        {
            return new InquiryDTO
            {
                InquiryId = Guid.NewGuid(),
                TotalPrice = 95,
                Currency = "PLN",
              
                ExpiringAt = DateTime.Now.AddDays(7),
                PriceBreakDown = new List<PriceBreakdown>
    {
        new PriceBreakdown { Amount = 80, Currency = "PLN", Description = "Podstawowa cena" },
        new PriceBreakdown { Amount = 10, Currency = "PLN", Description = "Podatek VAT" },
        new PriceBreakdown { Amount = 5, Currency = "PLN", Description = "Opłata za dostawę" },
    }
            };



        }

        



        public OfferRespondDTO AcceptOffer(OfferDTO offer)
        {
            return new OfferRespondDTO { OfferRequestId= Guid.NewGuid(), ValidTo = DateAndTime.Now };
        }


        public string Validate(DeliveryRequestDTO DRDTO)
        {
            if (DRDTO == null)
            {
                return ("Nieprawidłowe dane wejściowe"); // Bad Request, ponieważ model nie spełnia warunków
            }


            if (DRDTO.Package.Weight >= 1000)
            {
                return ("Zbyt duża waga paczki");
            }
            if (DRDTO.Package.Height > 1000 || DRDTO.Package.Length > 1000 || DRDTO.Package.Width > 1000)
            {
                return ("Zbyt duże wymiary paczki");
            }
            else
            {
                return "";
            }
        }
        public void SaveInDatabaseDelivery(OfferDTO DRDTO, OfferRespondDTO respond)
        {
            _deliveryRepository.SaveInDatabaseDelivery(DRDTO, respond);
        }
        public void SaveInDatabaseDeliveryRequest(DeliveryRequestDTO DRDTO, InquiryDTO response)
        {
            int SourceAddressID = _addressRepository.SaveInDatabaseDeliveryRequestSA(DRDTO, response);
            int DestinationAddressID = _addressRepository.SaveInDatabaseDeliveryRequestDA(DRDTO, response);
            int PackageID = _packageRepository.SaveInDatabasePackage(DRDTO, response);
            _inquiryRepository.SaveInDatabaseDeliveryRequest(DRDTO, response, SourceAddressID, DestinationAddressID, PackageID);
        }

        public async Task<Delivery> Find(Guid g)
        {
            var delivery = await _deliveryRepository.Find(g);
            return delivery;
        }
        public void DatabaseSave()
        {
            _deliveryRepository.DatabaseSave();
        }


    }

}
