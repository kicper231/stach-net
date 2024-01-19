using Domain.Abstractions;
using Domain.DTO;
using Domain.Model;
using Infrastructure;
using Microsoft.VisualBasic;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;


namespace Api.Service;

public class DatabaseService : IDatabaseInterface
{
    private readonly IAddressRepository _addressRepository;

    private readonly IPackageRepository _packageRepository;
    private readonly IDeliveryRequestRepository _inquiryRepository;
    private readonly IUserRepository _userRepository;
    private readonly IDeliveryRepository _deliveryRepository;


    public DatabaseService(IDeliveryRequestRepository repository, IUserRepository repositoryuser,
        IPackageRepository repositorypackage, IAddressRepository repositoryaddress,
        IDeliveryRepository delivery)
    {
        _inquiryRepository = repository;
        _userRepository = repositoryuser;
        _packageRepository = repositorypackage;
        _addressRepository = repositoryaddress;        
        _deliveryRepository = delivery;
    }

    public string Validate(DeliveryRequestDTO DRDTO)
    {
        if ( DRDTO == null)
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
        _inquiryRepository.SaveInDatabaseDeliveryRequest(DRDTO, response,SourceAddressID,DestinationAddressID,PackageID);
    }

}