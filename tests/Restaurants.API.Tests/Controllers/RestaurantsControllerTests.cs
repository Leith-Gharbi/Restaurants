using FluentAssertions;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Restaurants.API.Tests;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Seeders;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace Restaurants.API.Controllers.Tests;

public class RestaurantsControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly Mock<IRestaurantsRepository> _restaurantsRepositoryMock = new();


    public RestaurantsControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                services.Replace(ServiceDescriptor.Scoped(typeof(IRestaurantsRepository),
                                            _ => _restaurantsRepositoryMock.Object));


             
            });
        });
    }


    [Fact]
    public async Task GetById_ForNonExistingId_ShouldReturn404NotFound()
    {
        // arrange

        var id = 1123;

        _restaurantsRepositoryMock.Setup(m => m.GetByIdAync(id)).ReturnsAsync((Restaurant?)null);

        var client = _factory.CreateClient();

        // act
        var response = await client.GetAsync($"/api/restaurants/{id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetById_ForExistingId_ShouldReturn200Ok()
    {
        // arrange

        var id = 99;

        var restaurant = new Restaurant()
        {
            Id = id,
            Name = "Test",
            Description = "Test description"
        };

        _restaurantsRepositoryMock.Setup(m => m.GetByIdAync(id)).ReturnsAsync(restaurant);

        var client = _factory.CreateClient();

        // act
        var response = await client.GetAsync($"/api/restaurants/{id}");
        var restaurantDto = await response.Content.ReadFromJsonAsync<RestaurantDto>();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        restaurantDto.Should().NotBeNull();
        restaurantDto.Name.Should().Be("Test");
        restaurantDto.Description.Should().Be("Test description");
    }


    [Fact()]
    public async Task GetAll_ForValidRequest_Return200Ok()
    {
        //arrange

        var client = _factory.CreateClient();


        //act

       var result = await client.GetAsync("api/restaurants?pageNumber=1&PageSize=10");

        //assert 

        result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var content = await result.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty("because the response should contain a list of restaurants");
        // Optionally, you could also deserialize the content and assert its structure, e.g.:
        // var restaurants = JsonConvert.DeserializeObject<List<RestaurantDto>>(content);
        // restaurants.Should().NotBeNull();
        // restaurants.Count.Should().BeGreaterThan(0);
    }


    [Fact()]
    public async Task GetAll_ForInvalidRequest_Return400BadRequest()
    {
        //arrange

        var client = _factory.CreateClient();


        //act

        var result = await client.GetAsync("api/restaurants");

        //assert 

        result.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);

    }
}