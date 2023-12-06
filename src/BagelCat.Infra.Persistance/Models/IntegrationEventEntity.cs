using System;

namespace BagelCat.Infra.Persistance.Models;
public class IntegrationEventEntity
{
    public Guid Id { get; set; }
    public DateTime EventDate { get; set; }
    public string Name { get; set; }
    public string Data { get; set; }
}
