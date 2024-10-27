﻿using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistance;

namespace Restaurants.Infrastructure.Repositories
{
    internal class DishesRepository (RestaurantsDbContext dbContext) : IDishesRepository
    {
        public async Task<int> Create(Dish entity)
        {
            dbContext.Dishes.Add(entity);
            await dbContext.SaveChangesAsync();
            return entity.Id;

        }

        public async Task Delete(IEnumerable<Dish> dishes)
        {
           dbContext.Dishes.RemoveRange(dishes);
            await dbContext.SaveChangesAsync();

        }
    }
}