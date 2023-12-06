using BagelCat.Infra.Persistance;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BagelCat.Infra.EventingOutbox;

public class IntegrationEventSenderService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public IntegrationEventSenderService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            await PublishOutstandingIntegrationEvents(cancellationToken);
        }
    }

    private async Task PublishOutstandingIntegrationEvents(CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            using var scope = _scopeFactory.CreateScope();
            var bus = scope.ServiceProvider.GetRequiredService<IBus>();

            using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var integrationEvents = await dbContext.IntegrationEventOutbox.OrderBy(o => o.EventDate).ToListAsync();

            foreach (var integrationEvent in integrationEvents)
            {
                var message = JsonConvert.DeserializeObject(integrationEvent.Data, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                });

                if (message != null)
                    await bus.Publish(message);

                dbContext.IntegrationEventOutbox.Remove(integrationEvent);
                await dbContext.SaveChangesAsync();
            }

            await Task.Delay(1000, cancellationToken);
        }
        catch (Exception e)
        {
            await Task.Delay(10000, cancellationToken);
        }
    }
}