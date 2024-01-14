using Domain.Abstractions;
using Domain.Model;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

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


    


    public void Add(User user)
    {

        context.Users.Add(user);

        context.SaveChanges();
    }


   

    public void SaveChanges()
    {
        context.SaveChanges();
    }



    // async 

    public async Task<int> SaveChangesAsync()
    {
        return await context.SaveChangesAsync();
    }

    

}
