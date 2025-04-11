using System.ComponentModel.DataAnnotations;

namespace SimpleHandler;

public class ResponseModel<T> where T : class
{
    private ResponseModel()
    {
    }

    public ValidationResult? ValidationResult { get; set; } = null;

    public T? Payload { get; private init; }

    public bool IsError { get; private init; }

    public bool IsOk => !IsError && ValidationResult == null;

    /// <summary>
    /// Initializes a default instance of <see cref="Payload"/>
    /// </summary>
    /// <returns></returns>
    public static ResponseModel<T> Success()
    {
        return new ResponseModel<T>()
        {
            Payload = Activator.CreateInstance<T>()
        };
    }

    /// <summary>
    /// Sets <see cref="Payload"/>
    /// </summary>
    /// <returns></returns>
    public static ResponseModel<T> Success(T payload)
    {
        return new ResponseModel<T>()
        {
            Payload = payload
        };
    }

    public static ResponseModel<T> Failure()
    {
        return new ResponseModel<T>()
        {
            Payload = default(T),
            IsError = true,
        };
    }
}
