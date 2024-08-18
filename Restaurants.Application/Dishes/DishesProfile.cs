using AutoMapper;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Domain.Entities;


namespace Restaurants.Application.Dishes
{
    public class DishesProfile :Profile
    {

        public DishesProfile()
        {

            CreateMap<CreateDisheCommand, Dish>();
            CreateMap<Dish, DishDto>();

        }
    }
}
