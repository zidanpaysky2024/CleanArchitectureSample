using Architecture.Application.Products.IEntitySets;
using Architecture.Domain.Product.Entites;
using Architecture.Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Architecture.Persistence.EF.EntitySets
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
