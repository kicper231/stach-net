using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IPackageRepository
{
    List<Package> GetAll();
    Package GetById(int id);
    void Add(Package package);
}

