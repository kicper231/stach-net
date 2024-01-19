using Azure;
using Domain.DTO;
using Domain.Model;

public interface IAddressRepository
{
    public int SaveInDatabaseDeliveryRequestSA(DeliveryRequestDTO DRDTO, InquiryDTO response);
    public int SaveInDatabaseDeliveryRequestDA(DeliveryRequestDTO DRDTO,InquiryDTO response);
}