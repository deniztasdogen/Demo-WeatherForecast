
using System.Collections.Generic;

namespace BagelCat.Domain
{
    public interface IAggregateRoot<T>
    {
        public T Id { get; }

        public IReadOnlyList<IDomainEvent> DomainEvents { get; }
    }
}