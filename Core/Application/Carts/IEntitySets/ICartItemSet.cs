using Architecture.Application.Common.Abstracts.Persistence;
using Architecture.Domain.Cart.Entities;

namespace Architecture.Application.Carts.IEntitySets
{
    public interface ICartItemSet : IEntitySet<CartItem>
    {
    }
}
