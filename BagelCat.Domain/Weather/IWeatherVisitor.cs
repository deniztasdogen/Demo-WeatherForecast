using System;
using System.Threading;
using System.Threading.Tasks;

namespace BagelCat.Domain.Weather
{
    public interface IWeatherVisitor
    {
        void VisitWeather(Guid id, WeatherRecordDate recordDate, WeatherTemperature temperatureInCelcius, AggregateRootStatus status, CancellationToken cancellationToken = default);
    }
}
