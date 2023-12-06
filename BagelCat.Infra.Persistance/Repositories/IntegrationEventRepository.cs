using BagelCat.Infra.Persistance.Models;
using BagelCat.Messages;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace BagelCat.Infra.Persistance.Repositories;
public class IntegrationEventRepository
{
    private readonly ApplicationDbContext _dbContext;

    public IntegrationEventRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(IIntegrationEvent integrationEvent, bool isIndependent = false)
    {
        var toAdd = new IntegrationEventEntity
        {
            Id = Guid.NewGuid(),
            EventDate = DateTime.Now,
            Name = integrationEvent.GetType().FullName,
            Data = JsonConvert.SerializeObject(integrationEvent, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            })
        };

        _dbContext.Add(toAdd);

        await _dbContext.SaveChangesAsync();
    }
}
