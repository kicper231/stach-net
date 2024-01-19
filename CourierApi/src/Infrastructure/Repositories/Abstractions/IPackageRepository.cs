using Domain.DTO;
using Domain.Model;
namespace Domain.Abstractions;

public interface IPackageRepository
{
    public int SaveInDatabasePackage(DeliveryRequestDTO DRDTO, InquiryDTO response);
}