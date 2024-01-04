using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IAddressRepository
{
    List<Address> GetAll();
    Address GetById(int id);
    void Add(Address address);
}

