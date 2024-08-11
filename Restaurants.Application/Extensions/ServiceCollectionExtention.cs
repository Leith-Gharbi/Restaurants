using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Restaurants;

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Restaurants.Application.Extensions
{
    public static class ServiceCollectionExtentions
    {

        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IRestaurantsService, RestaurantsService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }


    }
}
