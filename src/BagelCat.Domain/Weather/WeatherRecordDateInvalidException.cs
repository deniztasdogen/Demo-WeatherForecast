using System;

namespace BagelCat.Domain.Weather
{
    public class WeatherRecordDateInvalidException : Exception
    {
        public WeatherRecordDateInvalidException(string message): base(message)
        {
            
        }
    }
}
