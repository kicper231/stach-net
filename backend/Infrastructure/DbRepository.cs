using Domain.Abstractions;
using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;




public class DbRepository : IUserRepository
{
   
    private readonly ShopperContext context;

  
    public DbRepository(ShopperContext context)
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


public class DbRequestRepository : IDeliveryRequestRepository
{
    private readonly ShopperContext _context;

    public DbRequestRepository(ShopperContext context)
    {
        _context = context;
    }

    public List<DeliveryRequest> GetDeliveryRequestsByUserId(string userId)
    {
        return _context.DeliveryRequests
            .Where(dr => dr.UserAuth0 == userId)
            .ToList();
    }

  
}



