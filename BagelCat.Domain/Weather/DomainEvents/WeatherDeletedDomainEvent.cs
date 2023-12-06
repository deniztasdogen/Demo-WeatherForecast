using System;

namespace BagelCat.Domain.Weather.Events
{
    public class WeatherDeletedDomainEvent : IDomainEvent
    {
        public Guid Id { get; }

        public WeatherDeletedDomainEvent(Guid id)
        {
            Id = id;
        }
    }
}
