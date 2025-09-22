using System.Net;

namespace dotnet_smk_telkom_2025.Infrastructure.Exceptions;

public class ConflictException : AppException
{
    public ConflictException(string message)
      : base(message, HttpStatusCode.Conflict)
    {
    }
}