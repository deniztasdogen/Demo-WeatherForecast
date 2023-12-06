using BagelCat.Domain.Weather.Events;
using BagelCat.Infra.Persistance.Repositories;
using BagelCat.Messages.Weather;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BagelCat.Application.DomainEventHandlers;
public class WeatherCreatedDomainEventHandler : IRequestHandler<WeatherCreatedDomainEvent>
{
    private readonly IntegrationEventRepository _integrationEventRepository;

    public WeatherCreatedDomainEventHandler(IntegrationEventRepository integrationEventRepository)
    {
        _integrationEventRepository = integrationEventRepository;
    }

    public async Task Handle(WeatherCreatedDomainEvent request, CancellationToken cancellationToken)
    {
        var integrationEvent = new WeatherCreatedIntegrationEvent(request.Id, request.WeatherRecordDate, request.TemperatureInCelcius);
        await _integrationEventRepository.AddAsync(integrationEvent);
    }
}
