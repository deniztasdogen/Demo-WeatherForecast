using System;

namespace BagelCat.Messages.Weather
{
    public class WeatherCreatedIntegrationEvent : IIntegrationEvent
    {
        public Guid Id { get; set; }
        public DateTime RecordDate { get; set; }
        public double TemperatureInCelcius { get; set; }

        public WeatherCreatedIntegrationEvent()
        {
            
        }

        public WeatherCreatedIntegrationEvent(Guid id, DateTime recordDate, double temperatureInCelcius)
        {
            Id = id;
            RecordDate = recordDate;
            TemperatureInCelcius = temperatureInCelcius;
        }
    }
}
