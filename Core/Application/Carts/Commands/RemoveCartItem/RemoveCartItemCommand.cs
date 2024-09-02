﻿using Architecture.Application.Carts;
using Architecture.Application.Common.Abstracts.Persistence;
using Architecture.Application.Common.Caching;
using Architecture.Application.Common.Messaging;
using Architecture.Application.Common.Models;
using Architecture.Domain.Constants;
using Architecture.Application.Common.Security;

namespace Architecture.Application.Carts.Commands.RemoveCartItem
{
    #region Request
    [Authorize(Policy = Permissions.Cart.Delete)]
    [Cache(KeyPrefix = nameof(CacheKeysPrefixes.Cart), CacheStore = CacheStore.All, ToInvalidate = true)]
    public record RemoveCartItemCommand : BaseCommand<bool>
    {
        public Guid Id { get; init; }
        public Guid CartId { get; init; }

    }
    #endregion

    #region Request Handler
    public class RemoveCartItemCommandHandler : BaseCommandHandler<RemoveCartItemCommand, bool>
    {
        #region Dependencies


        #endregion

        #region Constructor
        public RemoveCartItemCommandHandler(IServiceProvider serviceProvider, IApplicationDbContext dbContext)
           : base(serviceProvider, dbContext)
        {

        }
        #endregion

        #region Request Handle

        public override async Task<Response<bool>> HandleRequest(RemoveCartItemCommand request, CancellationToken cancellationToken)
        {
            var cart = await DbContext.Carts.Include(c => c.CartItems.Where(ci => ci.Id == request.Id))
                                            .FirstOrDefaultAsync(c => c.Id == request.CartId,
                                                                 cancellationToken);

            if (cart is null || cart.CartItems.Count == 0 || cart.UserId != Guid.Parse(request.UserId!))
            {
                return Response.Failure(CartsErrors.CartItemNotFoundError);
            }
            cart.RemoveCartItem(cart.CartItems.First());

            int affectedRows = await DbContext.SaveChangesAsync(cancellationToken);

            return affectedRows > 0 ? Response.Success(affectedRows) : Response.Failure(Error.InternalServerError);
        }
        #endregion
    }
    #endregion
}


