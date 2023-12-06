using BagelCat.Infra.Persistance.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BagelCat.Application.Commands;
public class DeleteWeatherCommand: IRequest
{
    public Guid Id { get; }

    public DeleteWeatherCommand(Guid id)
    {
        Id= id;
    }
}

public class DeleteWeatherCommandHandler : IRequestHandler<DeleteWeatherCommand>
{
    private readonly WeatherRepository _weatherRepository;

    public DeleteWeatherCommandHandler(WeatherRepository weatherRepository)
    {
        _weatherRepository = weatherRepository ?? throw new ArgumentNullException(nameof(weatherRepository));
    }

    public async Task Handle(DeleteWeatherCommand request, CancellationToken cancellationToken)
    {
        var weatherToDelete = await _weatherRepository.GetAsync(request.Id, cancellationToken);
        weatherToDelete.Delete();

        await _weatherRepository.SaveChangesAsync(weatherToDelete);
    }
}