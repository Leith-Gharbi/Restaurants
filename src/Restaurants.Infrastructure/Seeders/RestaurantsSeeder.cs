
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistance;

namespace Restaurants.Infrastructure.Seeders
{
    internal class RestaurantsSeeder(RestaurantsDbContext dbContext) : IRestaurantsSeeder
    {
        public async Task Seed()
        {

            if (dbContext.Database.GetPendingMigrations().Any())
            {

                await dbContext.Database.MigrateAsync();
            }
            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.Restaurants.Any())
                {

                    var restaurants = GetRestaurants();
                    dbContext.Restaurants.AddRange(restaurants);
                    await dbContext.SaveChangesAsync();

                }


                if (!dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    dbContext.Roles.AddRange(roles);
                    await dbContext.SaveChangesAsync();
                }
            }

        }
        private IEnumerable<IdentityRole> GetRoles()
        {
            List<IdentityRole> roles = [ 
                new(UserRoles.User) { NormalizedName = UserRoles.User.ToUpper() } , 
                new(UserRoles.Owner) { NormalizedName = UserRoles.Owner.ToUpper()},
                new(UserRoles.Admin) {NormalizedName =UserRoles.Admin.ToUpper()}
                ];
            return roles;

        }


        private IEnumerable<Restaurant> GetRestaurants()
        {
            List<Restaurant> restaurants = [
                new(){
                   Name="KFC",
                   Category="Fast Food",
                   Description="KFC ou PFK au Québec et au nord du Nouveau-Brunswick, est une chaîne de restauration rapide américaine. KFC est fondée au milieu du XXᵉ siècle par le colonel Harland Sanders et est connue pour ses recettes à base de poulet frit. KFC est une filiale de la multinationale américaine Yum!",
                   ContactEmail="contact@kfc.com",
                   HasDelivery=true,
                   Dishes=[
                       new(){
                           Name="Nashville Hit Chicken",
                           Description="Nashville Hot Chicken (10 pcs.)",
                           Price= 10.30M
                       },
                        new(){
                           Name="Chicken Nuggets",
                           Description="Chicken Nuggets (10 pcs.)",
                           Price= 10.30M
                       }
                       ],
                   Address=new(){
                       City="London",
                       PostalCode="WC2N 5DU",
                       Street="Cork St 5"
                   }
               },
                 new Restaurant(){
                   Name="MacDonald",
                   Category="Fast Food",
                   Description="McDonald's restaurants mainly serve hamburgers, cheeseburgers, chicken nuggets, burgers, french fries, breakfast items, soft drinks, milk shakes and desserts. They also have options such as salads, apples, milk, McDonald's Popular Picks",
                   ContactEmail="contact@macdonald.com",
                   HasDelivery=true,
                   Dishes=[
                       new(){
                           Name="Burger",
                           Description="Un burger, un accompagnement et une boisson",
                           Price= 12.30M
                       },

                       ],
                   Address=new(){
                       City="Rennes",
                       PostalCode="35700",
                       Street="41 square pedro flores"
                   }
               }
                ];

            return restaurants;
        }
    }

}
