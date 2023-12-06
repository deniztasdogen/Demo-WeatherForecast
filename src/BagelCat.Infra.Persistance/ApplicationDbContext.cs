using BagelCat.Infra.Persistance.Models;
using Microsoft.EntityFrameworkCore;

namespace BagelCat.Infra.Persistance;
public class ApplicationDbContext : DbContext
{
    public DbSet<WeatherEntity> WeatherEntities { get; set; }
    public DbSet<IntegrationEventEntity> IntegrationEventOutbox { get; set; }

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
}
