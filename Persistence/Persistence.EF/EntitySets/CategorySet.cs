using CleanArchitecture.Application.Categories.IEntitySets;
using CleanArchitecture.Domain.Product.Entites;
using CleanArchitecture.Persistence.EF;
using CleanArchitecture.Persistence.EF.EntitySets;

namespace Persistence.EF.EntitySets.ProductCatalogue
{
    public class CategorySet : EntitySet<Category>, ICategorySet
    {
        #region Constructor
        public CategorySet(ApplicationDbContext DbContext) : base(DbContext)
        {

        }
        #endregion

    }
}
