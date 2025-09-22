using System.Net;

namespace dotnet_smk_telkom_2025.Infrastructure.Exceptions;

public class ForbiddenException : AppException
{
    public ForbiddenException(string message)
      : base(message, HttpStatusCode.Forbidden)
    {
    }
}