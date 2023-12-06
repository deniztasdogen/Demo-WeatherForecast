//using BagelCat.Domain.Weather.Events;
//using BagelCat.Infra.Persistance.Repositories;
//using BagelCat.Messages.Weather;
//using MediatR;
//using System.Threading;
//using System.Threading.Tasks;

//namespace BagelCat.Application.DomainEventHandlers;
//public class WeatherDeletedDomainEventHandler : IRequestHandler<WeatherDeletedDomainEvent>
//{
//    private readonly IntegrationEventRepository _integrationEventRepository;

//    public WeatherDeletedDomainEventHandler(IntegrationEventRepository integrationEventRepository)
//    {
//        _integrationEventRepository = integrationEventRepository;
//    }

//    public async Task Handle(WeatherDeletedDomainEvent request, CancellationToken cancellationToken)
//    {
//        var integrationEvent = new WeatherDeletedIntegrationEvent(request.Id);
//        await _integrationEventRepository.AddAsync(integrationEvent);
//    }
//}
