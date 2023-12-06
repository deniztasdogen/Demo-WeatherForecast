using BagelCat.Infra.Persistance;
using Microsoft.EntityFrameworkCore;

namespace BagelCat.Api.Configurations;

public class PersistanceOptions
{
    public static string Section => "Persistance";
    public string Type { get; set; }
    public MssqlOptions Mssql { get; set; }

    public class MssqlOptions
    {
        public string ConnectionString { get; set; }
    }
}

public static class PersistanceConfiguration
{
    public static void AddPersistance(this WebApplicationBuilder? builder)
    {
        if(builder == null) throw new ArgumentNullException(nameof(builder));

        var option = new PersistanceOptions();
        builder.Configuration.Bind(PersistanceOptions.Section, option);

        switch (option.Type)
        {
            case "IntegrationTest":
                break;
            case "InMemory":
                builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("db"));
                break;
            case "Mssql":
                builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(option.Mssql.ConnectionString));
                break;
            default:
                throw new NotImplementedException($"{option.Type} is not implemented for Persistance");
        }
    }
}
