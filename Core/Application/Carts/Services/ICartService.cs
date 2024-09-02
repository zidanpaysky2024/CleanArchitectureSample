using Architecture.Application.Common.Abstracts.Business;
using Architecture.Domain.Cart.Entities;

namespace Architecture.Application.Carts.Services
{
    public interface ICartService : IService
    {
        Task<Cart> AddOrUpdateCartItemAsync(Cart cart, Guid productItemId, int count, CancellationToken cancellationToken = default);
        Task<Cart> AddUserCartAsync(Guid userId);
        Task<Cart?> GetUserCartAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
