using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Restaurants;

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace Restaurants.Application.Extensions
{
    public static class ServiceCollectionExtentions
    {

        public static void AddApplication(this IServiceCollection services)
        {

            var applicationAssembly = Assembly.GetExecutingAssembly();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(applicationAssembly)); // Register Mediator 
            services.AddAutoMapper(applicationAssembly);
            services.AddValidatorsFromAssembly(applicationAssembly)  // it will register all classes that direved from AbstractValidator<> in current assembly 
                   .AddFluentValidationAutoValidation();   // it will replace the endpoint validation 
        }


    }
}
