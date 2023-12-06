using BagelCat.Domain;
using System;

namespace BagelCat.Infra.Persistance.Exceptions;
public class UnknownDbOperationException: Exception
{
    public UnknownDbOperationException(AggregateRootStatus status): base ($"DB operation for {status} is now implemented.")
    {
        
    }
}
