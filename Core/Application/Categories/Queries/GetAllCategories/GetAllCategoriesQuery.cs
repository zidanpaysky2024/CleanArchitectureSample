using Architecture.Application.Common.Abstracts.Persistence;
using Architecture.Application.Common.Messaging;
using Architecture.Application.Common.Models;
using Architecture.Domain.Constants;
using Architecture.Domain.Product.Entites;
using Architecture.Application.Common.Security;

namespace Architecture.Application.Categories.Queries.GetAllCategories
{
    #region Request

    [Authorize(Policy = Permissions.Product.ReadCategories)]
    public record GetAllCategoriesQuery : BaseQuery<IReadOnlyCollection<Category>>
    {

    }
    #endregion

    #region Request Handler
    public class GetAllCategoriesQueryHandler : BaseQueryHandler<GetAllCategoriesQuery, IReadOnlyCollection<Category>>
    {
        #region Constructor
        public GetAllCategoriesQueryHandler(IServiceProvider serviceProvider, IApplicationDbContext dbContext)
           : base(serviceProvider, dbContext)
        {

        }
        #endregion

        #region Handel
        public override async Task<Response<IReadOnlyCollection<Category>>> HandleRequest(GetAllCategoriesQuery request,
                                                                                  CancellationToken cancellationToken)
        {
            if (request is null)
            {
                return Response.Failure<IReadOnlyCollection<Category>>(Error.NullArgument);
            }
            IReadOnlyCollection<Category> Items = await DbContext.Categories.ToListAsync(cancellationToken);

            return Response.Success(Items, Items.Count);
        }
        #endregion
    }
    #endregion
}
