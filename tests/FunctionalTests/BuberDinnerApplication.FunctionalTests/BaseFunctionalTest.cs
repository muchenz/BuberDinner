using BuberDinner.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinnerApplication.FunctionalTests;
public class BaseFunctionalTest : IClassFixture<FunctionalTestWebAppFactory>, IAsyncLifetime
{
    private readonly FunctionalTestWebAppFactory _factory;

    protected HttpClient Client { get; }
    public IServiceScope Scope { get; private set; } = null!;

    public IServiceProvider Services => Scope.ServiceProvider;

    public BaseFunctionalTest(FunctionalTestWebAppFactory factory)
    {
        _factory = factory;

        Client = factory.CreateClient();
        Scope = factory.Services.CreateScope();
    }

    public Task InitializeAsync()
    {
        var dBcontext = Scope.ServiceProvider.GetRequiredService<BuberDinnerDbContext>();
        dBcontext.Database.Migrate();
        return Task.CompletedTask;
    }

    public Task DisposeAsync()
    {
        Scope?.Dispose();
        return Task.CompletedTask;
    }
}
