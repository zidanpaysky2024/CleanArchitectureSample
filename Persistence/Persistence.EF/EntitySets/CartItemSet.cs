using Architecture.Application.Carts.IEntitySets;
using Architecture.Domain.Cart.Entities;

namespace Architecture.Persistence.EF.EntitySets
{
    public class CartItemSet : EntitySet<CartItem>, ICartItemSet
    {
        #region Constructor
        public CartItemSet(ApplicationDbContext DbContext) : base(DbContext)
        {

        }
        #endregion
    }
}
