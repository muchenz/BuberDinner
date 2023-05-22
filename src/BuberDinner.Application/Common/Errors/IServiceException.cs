using System.Net;

namespace BuberDinner.Api.Errors;

public interface IServiceException
{
    public HttpStatusCode  StatusCode { get; }
    public string ErrorMessage { get; } 
}
