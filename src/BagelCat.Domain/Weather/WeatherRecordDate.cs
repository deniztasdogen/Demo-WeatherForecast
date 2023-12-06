using System;

namespace BagelCat.Domain.Weather
{
    public class WeatherRecordDate : SimpleValueObject<DateTime>
    {
        public WeatherRecordDate(DateTime value) : base(value)
        {
        }

        protected override void ValidateValue(DateTime value)
        {
            
        }
    }
}
