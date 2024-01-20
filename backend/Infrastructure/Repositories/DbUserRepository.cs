using Domain.Abstractions;
using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class DbUserRepository : IUserRepository
{
    private readonly ShopperContext context;

    public DbUserRepository(ShopperContext context)
    {
        this.context = context;
    }

    public OperationResult<int> NumberOfUserLogins()
    {
        try
        {
            var sum = context.Users.Sum(a => a.NumberOfLogins);
            return OperationResult<int>.CreateSuccessResult(sum);
        }
        catch (Exception ex)
        {
            return OperationResult<int>.CreateFailure($"Error occurred retrieving the number of logins. {ex.Message} ");
        }
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


    // async 

    public async Task<int> SaveChangesAsync()
    {
        return await context.SaveChangesAsync();
    }

    public async Task AddAsync(User user)
    {
        await context.Users.AddAsync(user);
        context.SaveChanges();
    }

    public async Task<User?> GetBy0IdAsync(int? UserId)
    {
        if(UserId==null) return null;
        User? result = await context.Users.FirstOrDefaultAsync(u => u.UserId == UserId);

        return result;



    }
    public async Task<User?> GetByAuth0IdAsync(string Auth0Id)
    {
        User? result = await context.Users.FirstOrDefaultAsync(u => u.Auth0Id == Auth0Id);

        return result;



    }

}