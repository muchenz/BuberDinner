using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Application.Common.Errors;
public class  DuplicateEmailError : IError
{
    public List<IError> Reasons => new();
    public string Message => "Email already exists.";

    public Dictionary<string, object> Metadata => new() { { "StatusCode", HttpStatusCode.Conflict } };
}
