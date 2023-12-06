using BagelCat.Infra.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BagelCat.Application.Queries;
public class GetWeatherByIdQuery : IRequest<GetWeatherByIdQueryResponse>
{
    public Guid Id { get; }

    public GetWeatherByIdQuery(Guid id)
    {
        Id = id;
    }
}
public class GetWeatherByIdQueryResponse
{
    public Guid Id { get; }
    public DateTime RecordDate { get; }
    public double TemperatureInCelcius { get; }

    public GetWeatherByIdQueryResponse(Guid id, DateTime recordDate, double temperatureInCelcius)
    {
        Id = id;
        RecordDate = recordDate;
        TemperatureInCelcius = temperatureInCelcius;
    }
}

public class GetWeatherByIdQueryHandler : IRequestHandler<GetWeatherByIdQuery, GetWeatherByIdQueryResponse>
{
    private readonly ApplicationDbContext _dbContext;

    public GetWeatherByIdQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<GetWeatherByIdQueryResponse> Handle(GetWeatherByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _dbContext.WeatherEntities.FirstOrDefaultAsync(w => w.Id == request.Id);
        if(result == null)
        {
            throw new Exception("Something that should lead to 404.");
        }

        return new GetWeatherByIdQueryResponse(result.Id, result.RecordDate, result.TemperatureInCelcius);
    }
}