using System;

namespace BagelCat.Domain.Weather
{
    public class WeatherTemperatureOutOfRangeException: Exception
    {
        public WeatherTemperatureOutOfRangeException(string message): base(message)
        {
            
        }
    }
}
