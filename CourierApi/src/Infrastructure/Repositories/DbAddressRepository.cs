using Domain.DTO;
using Domain.Model;

namespace Infrastructure;

public class DbAddressRepository : IAddressRepository
{
    private readonly ShopperContext _context;

    public DbAddressRepository(ShopperContext context)
    {
        _context = context;
    }

    public int SaveInDatabaseDeliveryRequestSA(DeliveryRequestDTO DRDTO, InquiryDTO response)
    {
        var SourceAddressDB = new Address
        {
            HouseNumber = DRDTO.SourceAddress.HouseNumber,
            City = DRDTO.SourceAddress.City,
            Country = DRDTO.SourceAddress.Country,
            zipCode = DRDTO.SourceAddress.zipCode,
            Street = DRDTO.SourceAddress.Street,
            ApartmentNumber = DRDTO.SourceAddress.ApartmentNumber
        };
        _context.Addresses.Add(SourceAddressDB);
        _context.SaveChanges();
        return SourceAddressDB.AddressId;
    }
    public int SaveInDatabaseDeliveryRequestDA(DeliveryRequestDTO DRDTO, InquiryDTO response)
    {
        var DestinationAddressDB = new Address
        {
            HouseNumber = DRDTO.DestinationAddress.HouseNumber,
            City = DRDTO.DestinationAddress.City,
            Country = DRDTO.DestinationAddress.Country,
            zipCode = DRDTO.DestinationAddress.zipCode,
            Street = DRDTO.DestinationAddress.Street,
            ApartmentNumber = DRDTO.DestinationAddress.ApartmentNumber
        };
        _context.Addresses.Add(DestinationAddressDB);
        _context.SaveChanges();
        return DestinationAddressDB.AddressId;
    }
}