using System;

namespace BagelCat.Messages.Weather
{
    public class WeatherDeletedIntegrationEvent : IIntegrationEvent
    {
        public Guid Id { get; }

        public WeatherDeletedIntegrationEvent(Guid id)
        {
            Id = id;
        }
    }
}
