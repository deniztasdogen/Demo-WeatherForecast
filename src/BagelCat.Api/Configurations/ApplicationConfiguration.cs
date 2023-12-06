using BagelCat.Application.Commands;
using BagelCat.Infra.Persistance.Repositories;

namespace BagelCat.Api.Configurations;

public static class ApplicationConfiguration
{
    public static void AddApplication(this WebApplicationBuilder? builder)
    {
        if(builder == null) throw new ArgumentNullException(nameof(builder));

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateWeatherCommand).Assembly));

        builder.Services.AddScoped<WeatherRepository>();
    }
}
