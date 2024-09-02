

using Architecture.Application.Carts.IEntitySets;
using Architecture.Application.Categories.IEntitySets;
using Architecture.Application.Common.Abstracts.Account;
using Architecture.Application.Common.Abstracts.Persistence;
using Architecture.Application.Products.IEntitySets;
using Architecture.Infrastructure.Identity;
using Architecture.Persistence.EF.Configurations;
using Common.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Architecture.Persistence.EF
{
    public sealed class ApplicationDbContext : IdentityUserContext<ApplicationUser>, IApplicationDbContext
    {
        #region Properties
        private ICurrentUser CurrentUserService { get; }
        public IServiceProvider ServiceProvider { get; }

        #endregion

        #region Constructor
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
                         ICurrentUser currentUserService,
                         IServiceProvider serviceProvider) : base(options)
        {
            CurrentUserService = currentUserService;
            ServiceProvider = serviceProvider;
        }
        #endregion

        #region Entities Sets 

        IProductSet IApplicationDbContext.Products => ServiceProvider.GetInstance<IProductSet>();
        ICategorySet IApplicationDbContext.Categories => ServiceProvider.GetInstance<ICategorySet>();
        ICartSet IApplicationDbContext.Carts => ServiceProvider.GetInstance<ICartSet>();

        #endregion

        #region On Model Creating
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
            builder.ConfigureIdentity();
        }
        #endregion

        #region Save Changes
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
        #endregion
    }
}
