using Domain.Abstractions;
using Domain.Model;
using Infrastructure;
namespace Infrastructure;
public class DbUserRepository : IUserRepository
{

    private readonly ShopperContext context;


    public DbUserRepository(ShopperContext context)
    {
        this.context = context;
    }


    public List<User> GetAll()
    {

        return context.Users.ToList();
    }


    public User GetById(int id)
    {

        return context.Users.Find(id);
    }


    public void Add(User user)
    {

        context.Users.Add(user);

        context.SaveChanges();
    }

}
