using AutoMapper;
using FluentAssertions;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Domain.Entities;
using Xunit;


namespace Restaurants.Application.Restaurants.Dtos.Tests;

public class RestaurantProfileTests
{
    private IMapper _mapper;
    public RestaurantProfileTests()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<RestaurantProfile>();
        });
        _mapper = configuration.CreateMapper();
    }
    [Fact()]
    // TestMethode_Senario_ExpectedResult
    public void CreateMap_ForRestaurantToRestaurantDto_MapCorrectly()
    {
        // Arrange
        var restaurant = new Restaurant()
        {
            Id = 1,
            Name = "Test Restaurant",
            Description = "test Description",
            Category = "Test Category",
            HasDelivery = true,
            ContactEmail = "test@test.com",
            ContactNumber = "555-123-4567",
            Address = new Address()
            {
                City = "Metropolis",
                Street = "456 Gourmet Avenue",
                PostalCode = "54-321"
            }
        };

        // Act
        var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);

        // Assert
        restaurantDto.Should().NotBeNull();
        restaurantDto.Id.Should().Be(restaurant.Id);
        restaurantDto.Name.Should().Be(restaurant.Name);
        restaurantDto.Description.Should().Be(restaurant.Description);
        restaurantDto.Category.Should().Be(restaurant.Category);
        restaurantDto.HasDelivery.Should().Be(restaurant.HasDelivery);
        restaurantDto.City.Should().Be(restaurant.Address.City);
        restaurantDto.Street.Should().Be(restaurant.Address.Street);
        restaurantDto.PostalCode.Should().Be(restaurant.Address.PostalCode);



    }

    [Fact()]
    // TestMethode_Senario_ExpectedResult
    public void CreateMap_ForCreateRestaurantCommandToRestaurant_MapCorrectly()
    {
        // Arrange
        var command = new CreateRestaurantCommand()
        {
            Name = "Test Restaurant",
            Description = "test Description",
            Category = "Test Category",
            HasDelivery = true,
            ContactEmail = "test@test.com",
            ContactNumber = "555-123-4567",
            City = "Metropolis",
            Street = "456 Gourmet Avenue",
            PostalCode = "54-321"

        };

        // Act
        var restaurant = _mapper.Map<Restaurant>(command);


        // Assert
        restaurant.Should().NotBeNull();
        restaurant.Name.Should().Be(command.Name);
        restaurant.Description.Should().Be(command.Description);
        restaurant.Category.Should().Be(command.Category);
        restaurant.HasDelivery.Should().Be(command.HasDelivery);
        restaurant.ContactEmail.Should().Be(command.ContactEmail);
        restaurant.ContactNumber.Should().Be(command.ContactNumber);
        restaurant.Address.Should().NotBeNull();
        restaurant.Address.City.Should().Be(command.City);
        restaurant.Address.Street.Should().Be(command.Street);
        restaurant.Address.PostalCode.Should().Be(command.PostalCode);



    }

    [Fact()]
    // TestMethode_Senario_ExpectedResult
    public void CreateMap_ForUpdateRestaurantCommandToRestaurant_MapCorrectly()
    {
        // Arrange
        var command = new UpdateRestaurantCommand()
        {
            Name = "Test Restaurant",
            Description = "test Description",
            HasDelivery = true,
            Id = 1

        };

        // Act
        var restaurant = _mapper.Map<Restaurant>(command);

        // Assert
        restaurant.Should().NotBeNull();
        restaurant.Name.Should().Be(command.Name);
        restaurant.Description.Should().Be(command.Description);
        restaurant.HasDelivery.Should().Be(command.HasDelivery);
  



    }
}