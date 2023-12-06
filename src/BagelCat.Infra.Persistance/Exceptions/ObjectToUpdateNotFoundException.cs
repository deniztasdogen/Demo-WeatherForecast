
using BagelCat.Domain;
using System;

namespace BagelCat.Infra.Persistance.Exceptions;
public class ObjectToUpdateNotFoundException<T, K>: Exception where T : IAggregateRoot<K>
{
    public ObjectToUpdateNotFoundException(K id): base($"AggregateRoot {typeof(T).Name} with Id {id} is not found")
    {
        
    }
}
