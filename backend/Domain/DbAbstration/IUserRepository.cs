using Domain.Model;

namespace Domain.Abstractions
{
    public interface IUserRepository
    {
        List<User> GetAll();
        User GetByAuth0Id(string id);
        void Add(User user);
        public void SaveChanges();
        public OperationResult<int> NumberOfUserLogins();

        public Task<User> GetByAuth0IdAsync(string auth0Id);
        public Task AddAsync(User user);
        public Task<int> SaveChangesAsync();

    }



}
