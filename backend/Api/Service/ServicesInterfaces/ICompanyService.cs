using Domain.DTO;

namespace Api.Service;
public interface ICompanyService
    {
    public  Task<List<DTOIOD>> GetIODAsync();
    public Task<DTOIOD> GetIODAsync(Guid DeliveryId);
    public Task<List<DTOIOD>> GetAvailableIOD();
    public Task<List<DTOIOD>> GetCourierIOD(string Auth0Id);
    public Task<string> ChangeStatusByWorker(ChangeDeliveryStatusDTO changeDeliveryStatusDTO);
    public Task<string> ChangeStatusByCourier(ChangeDeliveryStatusDTO changeDeliveryStatusDTO);
}

