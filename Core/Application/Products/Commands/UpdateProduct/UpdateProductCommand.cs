﻿using Architecture.Application.Common.Abstracts.Persistence;
using Architecture.Application.Common.Messaging;
using Architecture.Application.Common.Models;
using Architecture.Application.Products.Commands.AddProduct;
using Architecture.Domain.Constants;
using Architecture.Domain.Product.Entites;
using Architecture.Application.Common.Security;

namespace Architecture.Application.Products.Commands.UpdateProduct
{
    #region Request

    [Authorize(Policy = Permissions.Product.Update)]
    public record UpdateProductCommand : BaseCommand<bool>
    {
        public Guid Id { get; init; }
        public string NameAr { get; init; } = string.Empty;
        public string NameEn { get; init; } = string.Empty;
        public string NameFr { get; init; } = string.Empty;

        public List<Guid> CategoriesIds { get; init; } = [];
        public List<ProductItemDto> ItemsList { get; init; } = [];

    }
    #endregion

    #region Request Handler
    public class UpdateProductCommandHandler : BaseCommandHandler<UpdateProductCommand, bool>
    {
        #region Dependencies


        #endregion

        #region Constructor
        public UpdateProductCommandHandler(
            IServiceProvider serviceProvider, IApplicationDbContext dbContext)
           : base(serviceProvider, dbContext)
        {

        }

        #endregion

        #region RequestHandle
        public override async Task<Response<bool>> HandleRequest(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await DbContext.Products.AsTracking()
                                                  .Include(p => p.ProductItems)
                                                  .Include(p => p.Categories)
                                                  .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
            if (product is null)
            {
                return Response.Failure(Error.ItemNotFound($"ProductId:{request.Id}"));
            }
            var category = await DbContext.Categories.AsTracking()
                                                     .Where(r => request.CategoriesIds.Contains(r.Id))
                                                     .ToListAsync(cancellationToken);
            product.Update(request.NameAr, request.NameEn, request.NameFr);


            product.UpdateCategory(category);
            product.UpdateProductItems(request.ItemsList.Select(s =>
            {
                return (ProductItem)s;
            }).ToList());


            DbContext.Products.Update(product);
            int affectedRows = await DbContext.SaveChangesAsync(cancellationToken);

            return affectedRows > 0 ? Response.Success(affectedRows) : Response.Failure(Error.InternalServerError);

        }
        #endregion
    }

    #endregion
}

