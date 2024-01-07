using Microsoft.AspNetCore.Mvc;
using Domain.Model;
using Domain.Abstractions;
using Domain.DTO;
using Api.Service;
using Microsoft.AspNetCore.Authorization;

namespace Api.Customers
{

    public  class LoginResult
    {
        public int NumberOfLogins { get; set; }
       
    }

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
        //[Authorize]

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


        [HttpGet("ActiveUsers")]
        public ActionResult<LoginResult> GetNumberOfLogins()
        {
            var result = userService.NumberOfLogins();
            if (result.Success)
            {
                return Ok(new LoginResult { NumberOfLogins = result.Data });
            }
            else
            {
                return StatusCode(500, result.ErrorMessage);
            }
        }




    }





    }




