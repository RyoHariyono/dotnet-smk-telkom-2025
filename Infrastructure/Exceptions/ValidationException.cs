using System.Net;

namespace dotnet_smk_telkom_2025.Infrastructure.Exceptions;

public class ValidationException : AppException
{
    public ValidationException(string message)
      : base(message, HttpStatusCode.UnprocessableEntity)
    {
    }
}