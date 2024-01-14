using Domain.Model;
using Domain.Abstractions;

namespace Api.Infrastructure
{
    public class FakeUserRepository : IUserRepository
    {
        private IDictionary<int, User> users;
        
        public FakeUserRepository()
        {
            var users = new List<User> 
            {
                new User { Id = 1, FirstName = "John", LastName = "Smith"},
                new User { Id = 2, FirstName = "Bob", LastName = "Smith"},
                new User { Id = 3, FirstName = "Ann", LastName = "Smith"},
            };

            this.users = users.ToDictionary(p=>p.Id);    
        }

        public List<User> GetAll()
        {
            return users.Values.ToList();
        }
    }
}