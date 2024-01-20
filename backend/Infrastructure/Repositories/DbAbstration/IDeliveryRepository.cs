using Domain.Model;

namespace Domain.Abstractions;

public interface IDeliveryRepository
{

    public Guid Add(Delivery delivery);
    public Task<Delivery> FindAsync(Guid id);
    public void Update(Delivery delivery);

    public  Task SaveChangesAsync();

    public List<Delivery> GetDeliveriesWithOffersAndRequests(IEnumerable<int> deliveryRequestIds);

    public Task<List<Delivery>> GetAllDeliveriesAsync();
    public Task<List<Delivery>> FindAcceptedDelivery();
    public Task<List<Delivery>> FindDeliveriesByCourierId(string courierId);

}

//05637187-c18d-445d-84d1-3a22f01a29af