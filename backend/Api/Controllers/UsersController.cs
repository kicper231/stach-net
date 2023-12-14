using Microsoft.AspNetCore.Mvc;
using Domain.Model;
using Domain.Abstractions;

namespace Api.Customers
{
    //[Route("api/customers")]
    //public class CustomersController : ControllerBase
    //{
    //    private readonly ICustomerRepository repository;

    //    public CustomersController(ICustomerRepository repository)
    //    {
    //        this.repository = repository;
    //    }

    //    // GET api/customers

    //    [HttpGet]
    //    public ActionResult<List<Customer>> Get()
    //    {
    //        var customers = repository.GetAll();

    //        return Ok(customers);
    //    }
        
    //    // GET api/customers/{id}
    //    [HttpGet("{id}", Name = "GetCustomerById")]
    //    public ActionResult<Customer> Get(int id)
    //    {
    //        var customer = repository.GetById(id);

    //        if (customer == null)
    //            return NotFound();

    //        return Ok(customer);
    //    }

    //    [HttpPost]
    //    public ActionResult<Customer> Post([FromBody] Customer customer)
    //    {
    //        repository.Add(customer);

    //        return CreatedAtRoute("GetCustomerById", new { Id = customer.Id} , customer);
    //    }
    //}


    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository repository;

        public UsersController(IUserRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public ActionResult<List<User>> Get()
        {
            var users = repository.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}", Name = "GetUserById")]
        public ActionResult<User> Get(int id)
        {
            var user = repository.GetById(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public ActionResult<User> Post([FromBody] User user)
        {
            repository.Add(user);
            return CreatedAtRoute("GetUserById", new { Id = user.Id }, user);
        }




       
    }



}
