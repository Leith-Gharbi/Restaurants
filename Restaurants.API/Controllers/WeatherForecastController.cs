using Microsoft.AspNetCore.Mvc;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{


    private readonly ILogger<WeatherForecastController> _logger;

    private readonly IWeatherForecastService _weatherForecastService;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService weatherForecastService)
    {
        _logger = logger;
        _weatherForecastService = weatherForecastService;
    }

    [HttpGet]
    public IEnumerable<WeatherForecast> Get([FromQuery] int count, [FromQuery] int minTemp, [FromQuery] int maxTemp)
    {
        return _weatherForecastService.Get(count, minTemp, maxTemp);

    }

    [HttpPost("generate")]

    public IActionResult Generate([FromBody] TemperatureRange temperatureRange, [FromQuery] int nbr)
    {
        if (nbr < 1 || (temperatureRange.maxTemp < temperatureRange.minTemp))
        {

            return BadRequest();
        }

        return Ok(_weatherForecastService.Get(nbr, temperatureRange.minTemp, temperatureRange.maxTemp));

    }


}

public class TemperatureRange
{

    public int minTemp { get; set; }
    public int maxTemp { get; set; }


}
