using BagelCat.Domain.Weather;
using BagelCat.Infra.Persistance.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BagelCat.Application.Commands;
public class CreateWeatherCommand: IRequest<CreateWeatherCommandResponse>
{
    public CreateWeatherCommand(DateTime recordDate, double temperatureInCelcius)
    {
        RecordDate = recordDate;
        TemperatureInCelcius = temperatureInCelcius;
    }

    public DateTime RecordDate { get; }
    public double TemperatureInCelcius { get; }
}

public class CreateWeatherCommandResponse
{
    public Guid Id { get; }

    public CreateWeatherCommandResponse(Guid id)
    {
        Id = id;
    }
}

public class CreateWeatherCommandHandler : IRequestHandler<CreateWeatherCommand, CreateWeatherCommandResponse>
{
    private readonly WeatherRepository _weatherRepository;

    public CreateWeatherCommandHandler(WeatherRepository weatherRepository)
    {
        _weatherRepository = weatherRepository;
    }

    public async Task<CreateWeatherCommandResponse> Handle(CreateWeatherCommand request, CancellationToken cancellationToken)
    {
        var weather = Weather.New(new WeatherRecordDate(request.RecordDate), new WeatherTemperature(request.TemperatureInCelcius));

        await _weatherRepository.SaveChangesAsync(weather, cancellationToken);

        return new CreateWeatherCommandResponse(weather.Id);
    }
}
