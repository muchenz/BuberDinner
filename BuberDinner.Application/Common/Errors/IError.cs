using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Application.Common.Errors;
public interface IError
{
    HttpStatusCode StatusCode { get; } 
    string ErrorMessage { get;  }
}
