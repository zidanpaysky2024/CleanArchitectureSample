﻿using Architecture.Application.Common.Abstracts.DomainEvent;
using Architecture.Application.Common.Abstracts.Persistence;
using Architecture.Domain.Product.Events;
using Microsoft.Extensions.Logging;

namespace Architecture.Application.Carts.EventHandlers
{
    public sealed class ProductAmountChangedEventHandler : BaseDomainEventHandler<ProductItemAmountChangedEvent>
    {
        #region Dependencies
        private IApplicationDbContext Context { get; }
        #endregion

        #region Constructor
        public ProductAmountChangedEventHandler(ILogger<ProductAmountChangedEventHandler> logger,
                                                IServiceProvider serviceProvider,
                                                IApplicationDbContext context) : base(logger, serviceProvider)
        {
            Context = context;
        }

        #endregion

        #region Event Handler

        public override async Task HandleEvent(ProductItemAmountChangedEvent notification,
                                         CancellationToken cancellationToken)
        {
            var carts = await Context.Carts.GetCartsContainsProductItem(notification.ProductItem, cancellationToken);
            var cartItems = carts.SelectMany(c => c.CartItems).ToList();
            var isRequiredExceededAmount = cartItems.Sum(ci => ci.Count) > notification.ProductItem.Amount;

            if (isRequiredExceededAmount)
            {
                await Context.Carts.DeleteCartItemsAsync(cartItems, cancellationToken);
            }
        }

        #endregion
    }
}
