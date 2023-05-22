using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Common.Behaviors;
using BuberDinner.Application.Services.Authentication.Common;
using ErrorOr;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Application
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(a=>a.RegisterServicesFromAssembly(typeof(DependecyInjection).Assembly));
            
             //services.AddScoped<IPipelineBehavior<RegisterCommand, ErrorOr<AuthenticationResult>>, ValidateRegisterCommandBehavior>();
            //services.AddScoped<IValidator<RegisterCommand>, RegisterCommandValidator>();

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidatinBehavior<,>));

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;

        }
    }
}
