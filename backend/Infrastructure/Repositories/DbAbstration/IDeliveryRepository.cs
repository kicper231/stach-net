using Domain.Model;

namespace Domain.Abstractions;

public interface IDeliveryRepository
{

    public Guid Add(Delivery delivery);
    public Task<Delivery> FindAsync(Guid id);
    public void Update(Delivery delivery);

    public  Task SaveChangesAsync();
}