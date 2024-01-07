using Microsoft.AspNetCore.Mvc;
using Domain.Model;
using Domain.Abstractions;
using Domain.DTO;
using Api.Service;
using Microsoft.AspNetCore.Authorization;

public class LoginResult
{
    public int NumberOfLogins { get; set; }

}
namespace Api.Controllers
{
    [Route("api/users")]
    [ApiController] 
    [Produces("application/json")] 
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
        [ProducesResponseType(StatusCodes.Status200OK)] 
        public ActionResult<List<User>> GetAll()
        {
            var users = repository.GetAll();
            return Ok(users);
        }

       
        [HttpGet("{id}", Name = "GetUserById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] 
        public ActionResult<User> GetById(string id)
        {
            var user = repository.GetByAuth0Id(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

      
        [HttpPost]
        [Authorize] 
        [ProducesResponseType(StatusCodes.Status201Created)] 
        [ProducesResponseType(StatusCodes.Status400BadRequest)] 
        public async Task<ActionResult<User>> Create([FromBody] DTO_UserFromAuth0 user)
        {
            var result = await userService.AddUserAsync(user);

            if (result.Success)
            {
                return CreatedAtRoute("GetUserById", new { id = result.user.UserId }, result.user);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        
        [HttpGet("ActiveUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

