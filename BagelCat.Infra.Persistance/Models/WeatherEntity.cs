using System;

namespace BagelCat.Infra.Persistance.Models;
public class WeatherEntity
{
    public Guid Id { get; set; }
    public DateTime RecordDate { get; set; }
    public double TemperatureInCelcius { get; set; }
}
