using Azure;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;


namespace Restaurants.API.Controllers.Tests;

public class RestaurantsControllerTests: IClassFixture<WebApplicationFactory<Program>>
{

    private readonly WebApplicationFactory<Program> _factory;
    public RestaurantsControllerTests(WebApplicationFactory<Program> factory)
    {
           _factory = factory;
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
    public async Task GetAll_ForInvalidRequest_Return400Ok()
    {
        //arrange

        var client = _factory.CreateClient();


        //act

        var result = await client.GetAsync("api/restaurants");

        //assert 

        result.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);

    }
}