using Microsoft.Extensions.DependencyInjection;
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
