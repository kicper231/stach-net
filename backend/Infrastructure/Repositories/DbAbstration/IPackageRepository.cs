using Domain.Model;
namespace Domain.Abstractions;

public interface IPackageRepository
{
    List<Package> GetAll();
    Package GetById(int id);
    void Add(Package package);
}