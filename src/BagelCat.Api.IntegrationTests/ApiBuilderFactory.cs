using BagelCat.Infra.Persistance;
using DotNet.Testcontainers.Builders;
using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MsSql;
using Testcontainers.RabbitMq;

namespace BagelCat.Api.IntegrationTests;
public class ApiBuilderFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    Random ports = new Random();

    private readonly MsSqlContainer _msSql;

    private readonly RabbitMqContainer _rabbitMq;

    public ApiBuilderFactory() : base()
    {
        var mssqlPort = ports.Next(10000, 50000);
        _msSql = new MsSqlBuilder()
        .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
        .WithPortBinding(mssqlPort, 1433)
        .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(1433))
        .Build();

        var rabbitMqPort = ports.Next(10000, 50000);
        _rabbitMq = new RabbitMqBuilder()
       .WithImage("rabbitmq:latest")
       .WithPortBinding(rabbitMqPort, 5672)
       .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(5672))
       .Build();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseConfiguration(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build());

        builder.ConfigureTestServices(services =>
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(_msSql.GetConnectionString()));

            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host($"{_rabbitMq.GetConnectionString()}");
                });
            });
        });
    }

    public async Task InitializeAsync()
    {
        await Task.WhenAll(_msSql.StartAsync(), _rabbitMq.StartAsync());
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await Task.WhenAll(_msSql.StopAsync(), _rabbitMq.StopAsync());
    }
}
