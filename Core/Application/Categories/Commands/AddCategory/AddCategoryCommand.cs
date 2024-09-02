using Architecture.Application.Common.Abstracts.Persistence;
using Architecture.Application.Common.Messaging;
using Architecture.Application.Common.Models;
using Architecture.Domain.Constants;
using Architecture.Domain.Product.Entites;
using AutoMapper;
using Architecture.Application.Common.Security;

namespace Architecture.Application.Categories.Commands.AddCategory
{
    #region Request
    [Authorize(Policy = Permissions.Product.AddCategory)]
    public record AddCategoryCommand : BaseCommand<Guid>
    {
        public required string NameAr { get; init; }
        public required string NameEn { get; init; }
        public required string NameFr { get; init; }
        public required string BriefAr { get; init; }
        public required string BriefEn { get; init; }
        public required string BriefFr { get; init; }
        public DateTime ApplyingDate { get; init; }

        #region Mapping
        public sealed class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<AddCategoryCommand, Category>();
            }
        }
        #endregion
    }

    #endregion

    #region Request Handler
    public class AddCategoryCommandHandler : BaseCommandHandler<AddCategoryCommand, Guid>
    {
        #region Dependencies 

        #endregion

        #region Constructor
        public AddCategoryCommandHandler(IServiceProvider serviceProvider,
                                         IApplicationDbContext dbContext)
            : base(serviceProvider, dbContext)
        {

        }

        #endregion

        #region Handel Request
        public async override Task<Response<Guid>> HandleRequest(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            Category category = Mapper.Map<Category>(request);
            await DbContext.Categories.AddAsync(category, cancellationToken);
            int affectedRows = await DbContext.SaveChangesAsync(cancellationToken);

            return affectedRows > 0 ? Response.Success(category.Id, affectedRows) : Response.Failure<Guid>(Error.InternalServerError);
        }
        #endregion
    }
    #endregion
}
