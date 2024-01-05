using Microsoft.AspNetCore.Mvc;
using Domain.Model;
using Domain.Abstractions;
using Domain.DTO;
using Api.Service;

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
        private readonly IUserService userService;
        public UsersController(IUserRepository repository, IUserService userService)
        {
            this.repository = repository;
            this.userService = userService;
        }

        [HttpGet]

        public ActionResult<List<User>> Get()
        {
            var users = repository.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}", Name = "GetUserById")]
        public ActionResult<User> Get(string id)
        {
            var user = repository.GetByAuth0Id(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost("auth0")]
        public ActionResult<User> Post([FromBody] DTO_UserFromAuth0 user)
            {
                var result = userService.AddUser(user);

                if (result.Success)
                {
                    if (result.StatusCode == 201)
                    {
                    return CreatedAtRoute("GetUserById", new { id = result.user.UserId }, result.user);
                }
                    return Ok(result.user);
                }
                else
                {
                    return StatusCode(result.StatusCode, result.Message);
                }
            }
        }





    }




