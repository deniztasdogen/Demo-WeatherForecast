using BagelCat.Domain.Weather.Events;
using System;
using System.Collections.Generic;

namespace BagelCat.Domain.Weather
{
    public class Weather : IAggregateRoot<Guid>
    {
        public Guid Id { get; }

        private AggregateRootStatus _status;

        private WeatherRecordDate _recordDate;

        private WeatherTemperature _temperatureInCelcius;

        private List<IDomainEvent> domainEvents = new List<IDomainEvent>();
        public IReadOnlyList<IDomainEvent> DomainEvents => domainEvents.AsReadOnly();


        private Weather(Guid id, WeatherRecordDate recordDate, WeatherTemperature temperatureInCelcius, AggregateRootStatus status)
        {
            Id = id;
            _recordDate = recordDate;
            _temperatureInCelcius = temperatureInCelcius;
            _status = status;

            if (status == AggregateRootStatus.New)
                domainEvents.Add(new WeatherCreatedDomainEvent(Id, _recordDate, _temperatureInCelcius));
        }

        public static Weather New(WeatherRecordDate recordDate, WeatherTemperature temperatureInCelcius)
        {
            if (recordDate < DateTime.Now)
                throw new WeatherRecordDateInvalidException("New records cannot have dates in the past.");

            return new Weather(Guid.NewGuid(), recordDate, temperatureInCelcius, AggregateRootStatus.New);
        }

        public static Weather Existing(Guid id, WeatherRecordDate recordDate, WeatherTemperature temperatureInCelcius)
        {
            return new Weather(id, recordDate, temperatureInCelcius, AggregateRootStatus.Existing);
        }

        public void Delete()
        {
            _status = AggregateRootStatus.MarkedForDeletion;
            domainEvents.Add(new WeatherDeletedDomainEvent(Id));
        }

        public void Accept(IWeatherVisitor visitor)
        {
            visitor.VisitWeather(Id, _recordDate, _temperatureInCelcius, _status);
        }
    }
}
