namespace BagelCat.Domain.Weather
{
    public sealed class WeatherTemperature : SimpleValueObject<double>
    {
        public WeatherTemperature(double value) : base(value)
        {

        }

        protected override void ValidateValue(double value)
        {
            if (value < -60 || value > 60)
                throw new WeatherTemperatureOutOfRangeException($"Temperature should be between -60 and 60 degrees");
        }
    }
}
