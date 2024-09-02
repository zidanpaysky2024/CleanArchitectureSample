﻿using CleanArchitecture.Application.Common.Security;
using CleanArchitecture.Domain.Product.Events;
using CleanArchitecture.Application.Common.Abstracts.Persistence;
using CleanArchitecture.Application.Common.Messaging;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Constants;

namespace CleanArchitecture.Application.Products.Commands.DeleteProduct
{
    #region Request
    [Authorize(Policy = Permissions.Product.Delete)]

    public record DeleteProductCommand : BaseCommand<bool>
    {
        public Guid Id { get; init; }
    }
    #endregion

    #region Request Handler
    public class DeleteProductCommandHandler : BaseCommandHandler<DeleteProductCommand, bool>
    {

        #region Dependencies

        #endregion

        #region Constructors
        public DeleteProductCommandHandler(IServiceProvider serviceProvider, IApplicationDbContext dbContext)
            : base(serviceProvider, dbContext)
        {

        }


        #endregion

        #region Request Handle
        public async override Task<Response<bool>> HandleRequest(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            int affectedRows = 0;

            var product = await DbContext.Products.GetByIdAsync(request.Id);

            if (product is null)
            {
                return Response.Failure(Error.ItemNotFound($"ProductId:{request.Id}"));
            }
            if (DbContext.Products.Delete(product))
            {
                product.AddDomainEvent(new ProductDeletedEvent(product));
                affectedRows = await DbContext.SaveChangesAsync(cancellationToken);
            }

            return affectedRows > 0 ? Response.Success(affectedRows) : Response.Failure(Error.InternalServerError);
        }
        #endregion
    }
    #endregion

}
