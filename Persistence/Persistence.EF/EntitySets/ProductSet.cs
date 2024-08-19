using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Products.IEntitySets;
using CleanArchitecture.Domain.Product.Entites;
using CleanArchitecture.Persistence.EF;
using CleanArchitecture.Persistence.EF.EntitySets;
using Microsoft.EntityFrameworkCore;

namespace Persistence.EF.EntitySets
{
    public class ProductSet : EntitySet<Product>, IProductSet
    {
        #region Constructor
        public ProductSet(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        #endregion

        #region Custom Methods
        public async Task<ProductItem> GetProductItemAsync(Guid productItemId)
        {
            //return await Context.Set<Product>()
            //                             .Include(p => p.ProductItems)
            //                             .SelectMany(p => p.ProductItems)
            //                             .FirstOrDefaultAsync(pi => pi.Id == productItemId)
            return await Context.Set<ProductItem>().AsTracking().FirstOrDefaultAsync(pi => pi.Id == productItemId)
                          ?? throw new NotFoundException("this item not found", productItemId);
        }
        #endregion
    }
}
