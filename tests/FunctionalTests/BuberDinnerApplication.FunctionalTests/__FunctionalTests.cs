using BuberDinner.Contracts.Authentication;
using BuberDinner.Infrastructure.Persistence;
using Docker.DotNet.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Testcontainers.MsSql;

namespace BuberDinnerApplication.FunctionalTests;
public sealed class MsSqlTests : IAsyncLifetime
{
    private readonly MsSqlContainer _msSqlContainer = new MsSqlBuilder()
        .WithPortBinding(6421, 1433)
        .WithPassword("MyTestPassword$$123")
        .WithName("master")
        .Build();

    public Task InitializeAsync()
    {
        return _msSqlContainer.StartAsync();
    }

    public Task DisposeAsync()
    {
        return _msSqlContainer.DisposeAsync().AsTask();
    }

    public sealed class IndexPageTests : IClassFixture<MsSqlTests>, IDisposable
    {
        private readonly WebApplicationFactory<Program> _webApplicationFactory;

        private readonly HttpClient _httpClient;

        public IndexPageTests(MsSqlTests fixture)
        {
            var clientOptions = new WebApplicationFactoryClientOptions();
            clientOptions.AllowAutoRedirect = false;

            _webApplicationFactory = new CustomWebApplicationFactory(fixture);
            _httpClient = _webApplicationFactory.CreateClient(clientOptions);


            var serviceDbContext = ((CustomWebApplicationFactory)_webApplicationFactory)
                .Scope.ServiceProvider.GetRequiredService<BuberDinnerDbContext>();
            serviceDbContext.Database.Migrate();
        }

        public void Dispose()
        {
            //_webApplicationFactory?.Dispose();
        }

        private sealed class CustomWebApplicationFactory : WebApplicationFactory<Program>
        {
            private readonly string _connectionString;

            public CustomWebApplicationFactory(MsSqlTests fixture)
            {
                _connectionString = fixture._msSqlContainer.GetConnectionString();
                Scope = Services.CreateScope();
            }

            protected override void ConfigureWebHost(IWebHostBuilder builder)
            {
                builder.ConfigureServices(services =>
                {
                    services.Remove(services.SingleOrDefault(service => typeof(DbContextOptions<BuberDinnerDbContext>) == service.ServiceType));
                    services.Remove(services.SingleOrDefault(service => typeof(DbConnection) == service.ServiceType));
                    services.AddDbContext<BuberDinnerDbContext>((_, option) => option.UseSqlServer(_connectionString));
                });
            }

            public IServiceScope Scope { get; private set; } = null!;
        }

        //[Fact]
        //public void Test1()
        //{
        //    // Arrange
        //    var defaultPage = await _httpClient.GetAsync("/")
        //        .ConfigureAwait(false);

        //    var document = await HtmlHelpers.GetDocumentAsync(defaultPage)
        //        .ConfigureAwait(false);

        //    // Act
        //    var form = (IHtmlFormElement)document.QuerySelector("form[id='messages']");
        //    var submitButton = (IHtmlButtonElement)document.QuerySelector("button[id='deleteAllBtn']");

        //    var response = await _httpClient.SendAsync(form, submitButton)
        //        .ConfigureAwait(false);

        //    // Assert
        //    Assert.Equal(HttpStatusCode.OK, defaultPage.StatusCode);
        //    Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        //    Assert.Equal("/", response.Headers.Location.OriginalString);
        //}

        //[Fact]
        public async Task Test1Async()
        {


            var request = new RegisterRequest(
                "Ala",
               "Kowalska",
                "ala@gmail.com",
                "alamakota");
            
            //var a = new StringContent("""

            //    {
            //        "firstName":"Ala",
            //        "lastName":"Kowalska",
            //        "email":"ala@gmail.com",
            //        "password":"alamakota"
            //    }

            //    """);


            var response  = await _httpClient.PostAsJsonAsync("auth/register", request);


            Assert.NotNull(response);
        }



    }
}


