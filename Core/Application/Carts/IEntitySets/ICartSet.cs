using Architecture.Application.Common.Abstracts.Persistence;
using Architecture.Domain.Cart.Entities;
using Architecture.Domain.Product.Entites;

namespace Architecture.Application.Carts.IEntitySets
{
    public interface ICartSet : IEntitySet<Cart>
    {
        ICartSet IncludeItemDetails();

        Task<List<Cart>> GetCartsContainsProductItem(ProductItem productItem, CancellationToken cancellationToken = default);

        Task<int> DeleteCartItemsAsync(List<CartItem> cartItems, CancellationToken cancellationToken = default);
    }
}
