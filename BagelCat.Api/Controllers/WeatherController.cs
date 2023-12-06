using BagelCat.Application.Commands;
using BagelCat.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BagelCat.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class WeatherController : ControllerBase
{
    private readonly IMediator _mediator;

    public WeatherController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost(Name = "CreateWeather")]
    public async Task<Guid> Create(CreateWeatherCommand command)
    {
        var result = await _mediator.Send(command);
        return result.Id;
    }

    [HttpGet(Name = "GetWeatherById")]
    public async Task<GetWeatherByIdQueryResponse> GetById([FromQuery] Guid id)
    {
        var result = await _mediator.Send(new GetWeatherByIdQuery(id));
        return result;
    }

    [HttpDelete(Name = "DeleteById")]
    public async Task DeleteById([FromQuery] Guid id)
    {
        await _mediator.Send(new DeleteWeatherCommand(id));
    }
}
