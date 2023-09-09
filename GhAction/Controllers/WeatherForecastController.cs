using Microsoft.AspNetCore.Mvc;

namespace GhAction.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get(CancellationToken cancellation)
    {
        try
        {
            Console.WriteLine("Get WeatherForecast requested");

            await Task.Delay(5000, cancellation);

            await Console.Out.WriteLineAsync("Carrying out Get WeatherForecast");

            var result = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

            await Console.Out.WriteLineAsync($"Number of results: " + result.Length);

            return result;
        }
        catch (TaskCanceledException cte) 
        {
            throw;
        }
    }
}
