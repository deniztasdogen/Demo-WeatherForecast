using BagelCat.Api.Configurations;
using BagelCat.Infra.Persistance;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddApplication();
builder.AddEventing();
builder.AddPersistance();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => { options.DisplayRequestDuration(); });

    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        if (dbContext.Database.IsSqlServer() && dbContext.Database.GetPendingMigrations().Any())
            dbContext.Database.Migrate();
    }
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

public partial class Program { }