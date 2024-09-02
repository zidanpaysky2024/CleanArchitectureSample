using Architecture.Application.Common.Abstracts.DomainEvent;
using Architecture.Domain.Product.Events;
using Microsoft.Extensions.Logging;

namespace Architecture.Application.Carts.EventHandlers
{
    public sealed class ProductCreatedEventHandler : BaseDomainEventHandler<ProductCreatedEvent>
    {
        #region Dependencies

        #endregion

        #region Constructor
        public ProductCreatedEventHandler(ILogger<ProductCreatedEventHandler> logger,
                                          IServiceProvider serviceProvider) : base(logger, serviceProvider)
        {

        }
        #endregion

        #region Handle Event
        public override Task HandleEvent(ProductCreatedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
        #endregion
    }
}
