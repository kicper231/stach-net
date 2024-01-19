using Domain.DTO;
using Domain.Model;

namespace Domain. Abstractions;

public interface IDeliveryRequestRepository
{
    public void SaveInDatabaseDeliveryRequest(DeliveryRequestDTO DRDTO, InquiryDTO response, int SourceAddressID, int DestinationAddressID, int PackageID);
}