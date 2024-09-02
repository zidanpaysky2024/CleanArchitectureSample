﻿using Architecture.Application.Common.Abstracts.Persistence;
using Architecture.Application.Common.Messaging;
using Architecture.Application.Common.Models;
using Architecture.Domain.Constants;
using Architecture.Application.Common.Security;

namespace Architecture.Application.Products.Commands.ChangeProductItemAmount
{
    #region Request
    [Authorize(Policy = Permissions.Product.ChangeProductItemAmount)]
    public record ChangeProductItemAmountCommand : BaseCommand<bool>
    {
        public Guid ProductId { get; init; }
        public Guid ProductItmeId { get; init; }
        public int Amount { get; init; }
    }
    #endregion

    #region Request Handler
    public class ChangeProductItemAmountCommandHandler : BaseCommandHandler<ChangeProductItemAmountCommand, bool>
    {
        #region Dependencies


        #endregion

        #region Constructor
        public ChangeProductItemAmountCommandHandler(
            IServiceProvider serviceProvider, IApplicationDbContext dbContext)
           : base(serviceProvider, dbContext)
        {

        }

        #endregion

        #region RequestHandle
        public override async Task<Response<bool>> HandleRequest(ChangeProductItemAmountCommand request, CancellationToken cancellationToken)
        {
            var product = await DbContext.Products.AsTracking()
                                                  .Include(p => p.ProductItems.Where(pi => pi.Id == request.ProductItmeId))
                                                  .FirstOrDefaultAsync(p => p.Id == request.ProductId, cancellationToken);

            if (product is null || product.ProductItems.Count == 0)
            {
                return Response.Failure(Error.ItemNotFound($"ProductId:{request.ProductId} contains the ProductItemid:{request.ProductItmeId}"));
            }

            product.ProductItems.First().ChangeAmount(request.Amount);
            DbContext.Products.Update(product);
            int affectedRows = await DbContext.SaveChangesAsync(cancellationToken);

            return affectedRows > 0 ? Response.Success(affectedRows) : Response.Failure(Error.InternalServerError);

        }
        #endregion
    }
    #endregion
}
