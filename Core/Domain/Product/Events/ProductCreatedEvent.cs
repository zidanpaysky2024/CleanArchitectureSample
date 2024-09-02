using Architecture.Domain.Common;

namespace Architecture.Domain.Product.Events
{
    public record ProductCreatedEvent(Entites.Product Product) : BaseDomainEvent
    {

    }
}
