
using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories
{
    public interface IRestaurantsRepository
    {
        Task<IEnumerable<Restaurant>> GetAllAsync();

        Task<Restaurant?> GetByIdAync(int id);

        Task Update(Restaurant restaurant);

        Task<int> Create(Restaurant restaurant);

        Task Delete(Restaurant restaurant);

        Task SaveChanges();
    }
}
