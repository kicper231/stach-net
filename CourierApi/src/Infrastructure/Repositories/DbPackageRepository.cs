using Domain.Model;
using Domain.Abstractions;
using Domain.DTO;
namespace Infrastructure;

public class DbPackageRepository : IPackageRepository
{
    private readonly ShopperContext _context;

    public DbPackageRepository(ShopperContext context)
    {
        _context = context;
    }
    public int SaveInDatabasePackage(DeliveryRequestDTO DRDTO, InquiryDTO response)
    {
        var PackageDB = new Package
        {
            Width = DRDTO.Package.Width,
            Height = DRDTO.Package.Height,
            Weight = DRDTO.Package.Weight,
            Length = DRDTO.Package.Length
        };

        _context.Packages.Add(PackageDB);
        _context.SaveChanges();
        return PackageDB.PackageId;
    }


}