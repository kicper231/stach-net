using Domain.Model;
using Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions
{
    public interface IUserRepository
    {
        List<User> GetAll();
        User GetByAuth0Id(string id);
        void Add(User user);
        public void SaveChanges();
        public OperationResult<int> NumberOfUserLogins();
    }



}
