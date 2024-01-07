using Domain.Abstractions;
using Domain.DTO;
using Domain.Model;
using Infrastructure;
namespace Api.Service;



public interface IUserService
{
    ServiceResult AddUser(DTO_UserFromAuth0 user);
    Task<ServiceResult> AddUserAsync(DTO_UserFromAuth0 user);
     OperationResult<int> NumberOfLogins();

}

public class UserService : IUserService
{
    private readonly IUserRepository repository;

    public OperationResult<int> NumberOfLogins()
    {
        var result = repository.NumberOfUserLogins();
        if (!result.Success)
        {
            
        }
        return result;
    }
    public UserService(IUserRepository repository)
    {
        this.repository = repository;
    }


    public ServiceResult AddUser(DTO_UserFromAuth0 user)
    {
        try
        {
            var existingUser = repository.GetByAuth0Id(user.Auth0Id);
            if (existingUser != null)
            {
                existingUser.NumberOfLogins++;
                repository.SaveChanges();
                return new ServiceResult(false, "Użytkownik już istnieje.", 409);
            }

            User AddUser = new User();
            AddUser.Auth0Id = user.Auth0Id;
            AddUser.FirstName = user.FirstName;
            AddUser.Email = user.Email;
            AddUser.LastName = user.LastName;
            AddUser.CreatedAt= DateTime.Now;
            AddUser.NumberOfLogins = 1;
            repository.Add(AddUser);

            //if (!success)
            //{
            //    return new ServiceResult(false, "Wystąpił problem podczas zapisywania użytkownika.", 500);
            //}

            return new ServiceResult(true, "Użytkownik został dodany.", 201, AddUser);
        }
        catch (Exception ex)
        {
            return new ServiceResult(false, "Wystąpił błąd: " + ex.Message, 500);
        }
    }

    public async Task<ServiceResult> AddUserAsync(DTO_UserFromAuth0 user)
    {
        try
        {
            // Asynchronicznie pobierz użytkownika.
            var existingUser = await repository.GetByAuth0IdAsync(user.Auth0Id);
            if (existingUser != null)
            {
                existingUser.NumberOfLogins++;
                await repository.SaveChangesAsync(); // Asynchroniczne zapisywanie zmian.
                return new ServiceResult(false, "Użytkownik już istnieje.", 409);
            }

            User AddUser = new User
            {
                Auth0Id = user.Auth0Id,
                FirstName = user.FirstName,
                Email = user.Email,
                LastName = user.LastName,
                CreatedAt = DateTime.Now,
                NumberOfLogins = 1
            };

            await repository.AddAsync(AddUser); // Załóżmy, że AddAsync to asynchroniczna wersja metody Add.

            return new ServiceResult(true, "Użytkownik został dodany.", 201, AddUser);
        }
        catch (Exception ex)
        {
            return new ServiceResult(false, "Wystąpił błąd: " + ex.Message, 500);
        }
    }



}

public class ServiceResult
{
    public bool Success { get; }
    public string Message { get; }
    public int StatusCode { get; }
    public User user { get; }

    public ServiceResult(bool success, string message, int statusCode, User data = null)
    {
        Success = success;
        Message = message;
        StatusCode = statusCode;
        user = data;
    }
}
