using System.Net;

namespace dotnet_smk_telkom_2025.Infrastructure.Exceptions;

public class InternalServerException : AppException
{
    public InternalServerException(string message)
      : base(message, HttpStatusCode.InternalServerError)
    {
    }
}