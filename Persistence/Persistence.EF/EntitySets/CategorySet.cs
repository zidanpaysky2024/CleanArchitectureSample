using Architecture.Application.Categories.IEntitySets;
using Architecture.Domain.Product.Entites;

namespace Architecture.Persistence.EF.EntitySets
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
