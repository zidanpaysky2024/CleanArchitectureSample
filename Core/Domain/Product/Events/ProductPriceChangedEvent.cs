using Architecture.Domain.Common;
using Architecture.Domain.Product.Entites;

namespace Architecture.Domain.Product.Events
{
    public record ProductPriceChangedEvent(ProductItem ProductDetails) : BaseDomainEvent
    {
    }
}
