using MediatR;


namespace Restaurants.Application.Dishes.Queries.GetDisheByIdForRestaurant
{
    public class GetDisheByIdForRestaurantQuery(int restaurantId, int dishId):IRequest<DishDto>
    {
        public int RestaurantId { get; set; } = restaurantId;
        public int DishId { get; set; } = dishId;
    }
}
