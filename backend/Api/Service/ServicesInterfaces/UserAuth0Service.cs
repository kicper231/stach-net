using Domain.Abstractions;
using Domain.DTO;

namespace Api.Service;

public interface IUserService
{
    ServiceResult AddUser(DTO_UserFromAuth0 user);
    Task<ServiceResult> AddUserAsync(DTO_UserFromAuth0 user);
    OperationResult<int> NumberOfLogins();
}
