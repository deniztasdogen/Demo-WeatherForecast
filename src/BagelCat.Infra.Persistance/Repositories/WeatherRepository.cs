using BagelCat.Domain.Weather;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BagelCat.Infra.Persistance.Repositories;
public class WeatherRepository
{
    private readonly IMediator _mediator;
    private readonly ApplicationDbContext _dbContext;

    public WeatherRepository(IMediator mediator, ApplicationDbContext dbContext)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<Weather> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.WeatherEntities.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        if (entity == null) { return null; }

        return Weather.Existing(entity.Id, new WeatherRecordDate(entity.RecordDate), new WeatherTemperature(entity.TemperatureInCelcius));
    }

    public async Task<List<Weather>> GetAllByRecordDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
    {
        var entities = await _dbContext.WeatherEntities.Where(e => e.RecordDate > startDate && e.RecordDate < endDate).ToListAsync(cancellationToken);

        return entities.Select(e => Weather.Existing(e.Id, new WeatherRecordDate(e.RecordDate), new WeatherTemperature(e.TemperatureInCelcius))).ToList();
    }

    public async Task SaveChangesAsync(Weather weather, CancellationToken cancellationToken = default)
    {
        var visitor = new WeatherVisitor(_dbContext);
        visitor.VisitWeather(weather);

        foreach (var domainEvent in weather.DomainEvents)
        {
            await _mediator.Send(domainEvent);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
