using Domain.Abstractions;
using Domain.DTO;
using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class DbRequestRepository : IDeliveryRequestRepository
{
    private readonly ShopperContext _context;


    public DbRequestRepository(ShopperContext context)
    {
        _context = context;
    }

    public void SaveInDatabaseDeliveryRequest(DeliveryRequestDTO DRDTO, InquiryDTO response, int SourceAddressID, int DestinationAddressID, int PackageID)
    {
        var DeliveryRequestDB = new DeliveryRequest
        {
            PackageId = PackageID,
            SourceAddressId = SourceAddressID,
            DestinationAddressId = DestinationAddressID,
            DeliveryDate = DRDTO.DeliveryDate,
            RequestDate = DateTime.Now,
            DeliveryRequestGuid = response.InquiryId,
            WeekendDelivery = DRDTO.WeekendDelivery,
            Priority = (DRDTO.Priority == true) ? PackagePriority.High : PackagePriority.Low,


        };
        _context.DeliveryRequests.Add(DeliveryRequestDB);
        _context.SaveChanges();
    }
}