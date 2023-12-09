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






