using BuberDinner.Api.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Application.Common.Errors;
public class DuplicateEmailExeption : Exception, IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

    public string ErrorMessage => "Email alread exist.";
}
