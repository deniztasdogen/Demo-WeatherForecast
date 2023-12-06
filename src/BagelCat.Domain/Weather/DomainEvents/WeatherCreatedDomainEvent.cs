using System;

namespace BagelCat.Domain.Weather.Events
{
    public class WeatherCreatedDomainEvent : IDomainEvent
    {
        public string EventName => typeof(WeatherCreatedDomainEvent).FullName;

        public Guid Id { get; }
        public WeatherRecordDate WeatherRecordDate { get; }
        public WeatherTemperature TemperatureInCelcius { get; }

        public WeatherCreatedDomainEvent(Guid id, WeatherRecordDate recordDate, WeatherTemperature temperatureInCelcius)
        {
            Id = id;
            WeatherRecordDate = recordDate;
            TemperatureInCelcius = temperatureInCelcius;
        }
    }
}
