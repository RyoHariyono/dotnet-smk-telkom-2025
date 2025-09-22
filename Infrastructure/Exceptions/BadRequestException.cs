using System.Net;

namespace dotnet_smk_telkom_2025.Infrastructure.Exceptions;

public class BadRequestException : AppException
{
    public BadRequestException(string message)
      : base(message, HttpStatusCode.BadRequest)
    {
    }
}