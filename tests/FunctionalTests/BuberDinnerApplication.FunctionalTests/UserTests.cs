using BuberDinner.Contracts.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BuberDinnerApplication.FunctionalTests;
public class UserTests : BaseFunctionalTest
{
    public UserTests(FunctionalTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Test1Async()
    {


        var request2 = new RegisterRequest(
            "Ala",
           "Kowalska",
            "ala@gmail.com",
            "alamakota");


        var a = JsonSerializer.Serialize(request2);

        //var request = new
        //    {
        //        FirstName = "Ala",
        //        LastName= "Kowalska",
        //        Email= "ala@gmail.com",
        //        Password= "alamakota"
        //    };


        var response = await Client.PostAsJsonAsync("auth/register", request2);


        Assert.NotNull(response);
    }
}
