using Domain.Model;

public interface IPackageRepository
{
    List<Package> GetAll();
    Package GetById(int id);
    void Add(Package package);
}