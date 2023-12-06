using BagelCat.Application.Commands;
using System.Net.Http.Json;

namespace BagelCat.Api.IntegrationTests;

public class WeatherTestsCopyCopyCopy : IClassFixture<ApiBuilderFactory>
{
    private readonly ApiBuilderFactory _factory;

    public WeatherTestsCopyCopyCopy(ApiBuilderFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task CreateWeather_Success_WithValidInputs()
    {
        var client = _factory.CreateClient();

        var command = new CreateWeatherCommand(DateTime.Now.AddDays(1), 10);

        var content = JsonContent.Create(command);
        var response = await client.PostAsync("api/Weather", content);

        Assert.True(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task CreateWeather_Success_WithValidInputs2()
    {
        var client = _factory.CreateClient();

        var command = new CreateWeatherCommand(DateTime.Now.AddDays(1), 10);

        var content = JsonContent.Create(command);
        var response = await client.PostAsync("api/Weather", content);

        Assert.True(response.IsSuccessStatusCode);
    }
}