using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Authorization;
using Restaurants.Infrastructure.Persistance;
using Restaurants.Infrastructure.Repositories;
using Restaurants.Infrastructure.Seeders;

namespace Restaurants.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services ,IConfiguration configuration)
        {

            var connectionString = configuration.GetConnectionString("RestaurantsDb");
            services.AddDbContext<RestaurantsDbContext>(options => options.UseSqlServer(connectionString).EnableSensitiveDataLogging());
            services.AddScoped<IRestaurantsSeeder, RestaurantsSeeder>();
            services.AddScoped<IRestaurantsRepository, RestaurantsRepository>(); 
            services.AddScoped<IDishesRepository, DishesRepository>();

            services.AddIdentityApiEndpoints<User>()
                .AddRoles<IdentityRole>()
                .AddClaimsPrincipalFactory<RestaurantsUserClaimsPrincipalFactory>() // to use custom claim principal 
                .AddEntityFrameworkStores<RestaurantsDbContext>();



            services.AddAuthorizationBuilder().AddPolicy(PolicyNames.HasNationality, builder => builder.RequireClaim(AppClaimTypes.Nationality, "Tunisian" ,"German"));  // allow authorize attribute with policy value 
        }

      
    }
}
