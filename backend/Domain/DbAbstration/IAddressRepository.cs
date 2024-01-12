using Domain.Model;


public interface IAddressRepository
{
    List<Address> GetAll();
    Address GetById(int id);
    void Add(Address address);
}

