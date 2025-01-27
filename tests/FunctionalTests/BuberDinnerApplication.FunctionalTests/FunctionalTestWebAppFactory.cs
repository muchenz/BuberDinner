﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Testcontainers.MsSql;
using BuberDinner.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using Microsoft.Extensions.DependencyInjection;

namespace BuberDinnerApplication.FunctionalTests;
public class FunctionalTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{

    private readonly MsSqlContainer _msSqlContainer = new MsSqlBuilder()
        .WithPortBinding(6422, 1433)
        .WithPassword("MyTestPassword$$123")
        .WithName("master2")
        .Build();



    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.Remove(services.SingleOrDefault(service => typeof(DbContextOptions<BuberDinnerDbContext>) == service.ServiceType));
            services.Remove(services.SingleOrDefault(service => typeof(DbConnection) == service.ServiceType));
            services.AddDbContext<BuberDinnerDbContext>((_, option) =>
                option.UseSqlServer(_msSqlContainer.GetConnectionString()));



        });
    }


    // Script-Migration -i
    // dotnet ef migrations script -i -p .\src\BuberDinner.Api\ -o .\tests\FunctionalTests\BuberDinnerApplication.FunctionalTests\migration.sql

    public async Task InitializeAsync()
    {
        await _msSqlContainer.StartAsync();



        //-------------------- place to run sql scripts eg. migration, seed data etc - runs once

        //var  sql = await File.ReadAllTextAsync("../../../migration.sql");

        //await _msSqlContainer.ExecScriptAsync(sql);

        //--------

    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await _msSqlContainer.StopAsync();


    }
}
