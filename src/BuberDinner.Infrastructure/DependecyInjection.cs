using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Application.Common.Interfaces.Services;
using BuberDinner.Infrastructure.Authentication;
using BuberDinner.Infrastructure.Interceptors;
using BuberDinner.Infrastructure.Job;
using BuberDinner.Infrastructure.Persistence;
using BuberDinner.Infrastructure.Persistence.Repositories;
using BuberDinner.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Infrastructure;

public static class DependecyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddAuth(services, configuration);
        AddPersistance(services, configuration);
        AddJob(services, configuration);

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;

    }
    private static IServiceCollection AddPersistance(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default");
        services.AddDbContext<BuberDinnerDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<InsertOutBoxMessagesInterceptor>();
        services.AddScoped<PublishDoimainEventsIntercetor>();
        services.AddScoped<SoftDeleteInterceptor>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IMenuRepository, MenuRepository>();
        services.AddScoped<IOutBoxMessageRepository, OutBoxMessageRepository>();
        return services;
    }

    private static void AddAuth(IServiceCollection services, IConfiguration configuration)
    {

        var jwtSetting = new JwtSettings();
        configuration.Bind(nameof(JwtSettings), jwtSetting);

        //services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));   //ioptions pattern

        services.AddSingleton(Options.Create(jwtSetting));

        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddAuthentication(defaultScheme:JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSetting.Issuer,
                ValidAudience = jwtSetting.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.SecretKey))

            });
    }

    private static void AddJob(IServiceCollection services, IConfiguration configuration)
    {
        services.AddQuartz(configure =>
        {
            var jobKey = new JobKey(nameof(ProcessOutBoxMessagesJob));

            configure.AddJob<ProcessOutBoxMessagesJob>(jobKey)
            .AddTrigger(trigger => trigger.ForJob(jobKey).WithSimpleSchedule(
                schedule => schedule.WithIntervalInSeconds(5).RepeatForever()));

            //configure.UseMicrosoftDependencyInjectionJobFactory();

        });

        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
    }
}
