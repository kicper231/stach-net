using Domain.Abstractions;
using Domain.Model;
using Infrastructure;

namespace Infrastructure;
public class DbUserRepository : IUserRepository
{

    private readonly ShopperContext context;

    public OperationResult<int> NumberOfUserLogins()
    {
        try
        {
            int sum = context.Users.Sum(a => a.NumberOfLogins);
            return OperationResult<int>.CreateSuccessResult(sum);
        }
        catch (Exception ex)
        {
           
            return OperationResult<int>.CreateFailure("Error occurred retrieving the number of logins.");
        }
    }

    public DbUserRepository(ShopperContext context)
    {
        this.context = context;
    }


    public List<User> GetAll()
    {

        return context.Users.ToList();
    }


    public User GetByAuth0Id(string Auth0Id)
    {

        return context.Users.FirstOrDefault(u => u.Auth0Id == Auth0Id);
    }


    public void Add(User user)
    {

        context.Users.Add(user);

        context.SaveChanges();
    }


   

    public void SaveChanges()
    {
        context.SaveChanges();
    }

}
