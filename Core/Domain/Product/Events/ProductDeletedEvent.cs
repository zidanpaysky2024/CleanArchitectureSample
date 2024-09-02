using Architecture.Domain.Common;

namespace Architecture.Domain.Product.Events
{
    public record ProductDeletedEvent(Entites.Product Product) : BaseDomainEvent
    {
    }
}
