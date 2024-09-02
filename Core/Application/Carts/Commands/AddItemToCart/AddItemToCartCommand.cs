using Architecture.Application.Carts.Services;
using Architecture.Application.Common.Abstracts.Persistence;
using Architecture.Application.Common.Caching;
using Architecture.Application.Common.Messaging;
using Architecture.Application.Common.Models;
using Architecture.Domain.Cart.Entities;
using Architecture.Domain.Constants;
using Architecture.Application.Common.Security;

namespace Architecture.Application.Carts.Commands.AddItemToCart
{
    #region Request
    [Authorize(Policy = Permissions.Cart.Add)]
    [Cache(KeyPrefix = nameof(CacheKeysPrefixes.Cart), CacheStore = CacheStore.All, ToInvalidate = true)]
    public record AddItemToCartCommand : BaseCommand<Guid>
    {
        public Guid ProductItemId { get; init; }
        public Guid? CartId { get; init; }
        public int Count { get; init; }
    }
    #endregion

    #region Rquest Handler
    public sealed class AddItemToCartCommandHandler : BaseCommandHandler<AddItemToCartCommand, Guid>
    {
        #region Dependencies

        ICartService CartService { get; }

        #endregion

        #region Constructor
        public AddItemToCartCommandHandler(IServiceProvider serviceProvider,
                                           IApplicationDbContext dbContext,
                                           ICartService cartService)
           : base(serviceProvider, dbContext)
        {
            CartService = cartService;
        }
        #endregion

        #region Request Handle
        public async override Task<Response<Guid>> HandleRequest(AddItemToCartCommand request, CancellationToken cancellationToken)
        {
            Guid userId = Guid.Parse(request.UserId!);
            Cart? cart = request.CartId.HasValue ? await CartService.GetUserCartAsync(userId, cancellationToken) : null;

            cart ??= await CartService.AddUserCartAsync(userId);
            await CartService.AddOrUpdateCartItemAsync(cart, request.ProductItemId, request.Count, cancellationToken);
            int affectedRows = await DbContext.SaveChangesAsync(cancellationToken);

            return affectedRows > 0 ? Response.Success(cart.Id, affectedRows) : Response.Failure<Guid>(Error.InternalServerError);
        }

        #endregion
    }
    #endregion
}
