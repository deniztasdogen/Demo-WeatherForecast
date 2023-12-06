using BagelCat.Infra.EventingOutbox;
using BagelCat.Infra.Persistance.Repositories;
using MassTransit;

namespace BagelCat.Api.Configurations;

public class EventingOptions
{
    public static string Section => "Event";

    public string Type { get; set; }

    public RabbitMqOptions RabbitMq { get; set; }

    public class RabbitMqOptions
    {
        public string Host { get; set; }
        public string Port { get; set; } = "5672";
        public string Username { get; set; }
        public string Password { get; set; }
    }

}

public static class EventingConfiguration
{
    public static void AddEventing(this WebApplicationBuilder? builder)
    {
        if(builder == null) throw new ArgumentNullException(nameof(builder));

        builder.Services.AddScoped<IntegrationEventRepository>();
        builder.Services.AddHostedService<IntegrationEventSenderService>();

        var option = new EventingOptions();
        builder.Configuration.Bind(EventingOptions.Section, option);

        switch (option.Type)
        {
            case "IntegrationTest":
                break;
            case "InMemory":
                builder.Services.AddMassTransit(x =>
                {
                    x.UsingInMemory();
                });
                break;
            case "RabbitMq":
                builder.Services.AddMassTransit(x =>
                {
                    x.UsingRabbitMq((context, cfg) =>
                    {
                        cfg.Host($"{option.RabbitMq.Host}:{option.RabbitMq.Port}", "/", h =>
                        {
                            h.Username(option.RabbitMq.Username);
                            h.Password(option.RabbitMq.Password);
                        });
                    });
                });
                break;
            default:
                throw new NotImplementedException($"{option.Type} is not implemented for MassTransit");
        }
    }
}
