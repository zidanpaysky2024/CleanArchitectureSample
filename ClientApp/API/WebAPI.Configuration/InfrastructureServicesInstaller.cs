using Architecture.Application.Common.Abstracts.Caching;
using Architecture.Application.Common.Abstracts.Persistence;
using Architecture.Infrastructure.Caching;
using Architecture.Infrastructure.Caching.RedisSetupConfigurationOptions;
using Architecture.Persistence.EF;
using Architecture.Persistence.EF.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Architecture.WebAPI.Configuration
{
    public class InfrastructureServicesInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IApplicationDbContext, ApplicationDbContext>((sp, options) =>
               options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
               .AddInterceptors(sp.GetServices<ISaveChangesInterceptor>()));
            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
            services.ConfigureOptions<RedisOptionsSetup>();


            services.AddStackExchangeRedisCache(ConfigureRedis(configuration));
            services.ConfigureOptions<RedisConfigurationOptionsSetup>();

            services.AddSingleton<IInMemoryCacheService, InMemoryCacheService>();
            services.AddSingleton<IDistributedCacheService, DistributedCacheService>();
            services.AddSingleton<ICrossCacheService, CrossCacheService>();
            services.AddSingleton<IHostedService, InvalidationCacheChannelSubscriber>();
            services.AddScoped<ApplicationDbContextInitializer>();
        }
        private static Action<RedisCacheOptions> ConfigureRedis(IConfiguration configuration)
        {
            return option =>
            {
                RedisOptions redisOPtions = new();
                RedisOptionsSetup optionSetup = new(configuration);
                optionSetup.Configure(redisOPtions);

                option.ConfigurationOptions = new StackExchange.Redis.ConfigurationOptions
                {
                    EndPoints = { { redisOPtions.Host ?? throw new InvalidOperationException("Redis Host can't be null"), redisOPtions.Port } },
                    User = redisOPtions.User,
                    Password = redisOPtions.Password,
                    Ssl = redisOPtions.Ssl
                };
            };
        }
    }
}
