using Architecture.Application.Common.Abstracts.Persistence;
using Architecture.Domain.Product.Entites;

namespace Architecture.Application.Products.IEntitySets
{
    public interface IProductSet : IEntitySet<Product>
    {
        Task<ProductItem> GetProductItemAsync(Guid productItemId);
    }
}
