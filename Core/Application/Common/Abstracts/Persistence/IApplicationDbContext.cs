using Architecture.Application.Carts.IEntitySets;
using Architecture.Application.Categories.IEntitySets;
using Architecture.Application.Products.IEntitySets;

namespace Architecture.Application.Common.Abstracts.Persistence
{
    public interface IApplicationDbContext
    {
        IProductSet Products { get; }
        ICategorySet Categories { get; }
        ICartSet Carts { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
        int SaveChanges();
    }
}
