using BagelCat.Domain;
using BagelCat.Domain.Weather;
using BagelCat.Infra.Persistance.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BagelCat.Infra.Persistance.Repositories;
public class WeatherVisitor : IWeatherVisitor
{
    private readonly ApplicationDbContext _dbContext;

    public WeatherVisitor(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public void VisitWeather(Weather weather)
    {
        weather.Accept(this);
    }

    void IWeatherVisitor.VisitWeather(Guid id, WeatherRecordDate recordDate, WeatherTemperature temperatureInCelcius, AggregateRootStatus status, CancellationToken cancellationToken = default)
    {
        var entity = _dbContext.WeatherEntities.SingleOrDefault(w => w.Id == id);
        if(entity == null)
        {
            entity = new WeatherEntity();
        }

        entity.Id = id;
        entity.RecordDate = recordDate;
        entity.TemperatureInCelcius = temperatureInCelcius;

        switch(status)
        {
            case AggregateRootStatus.MarkedForDeletion:
                _dbContext.Remove(entity);
                break;
            case AggregateRootStatus.New:
                _dbContext.Add(entity);
                break;
            case AggregateRootStatus.Existing:
                _dbContext.Update(entity);
                break;
            default:
                throw new NotImplementedException($"{status} is not implemented for DB operation.");
        }
    }
}
