

namespace Domain.Abstractions;

public class OperationResult<T>
{
    public T? Data { get; set; }
    public bool Success { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;

    public static OperationResult<T> CreateSuccessResult(T data)
    {
        return new OperationResult<T> { Success = true, Data = data };
    }

    public static OperationResult<T> CreateFailure(string errorMessage)
    {
        return new OperationResult<T> { Success = false, ErrorMessage = errorMessage };
    }
}
