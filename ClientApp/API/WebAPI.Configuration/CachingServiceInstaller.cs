using Architecture.Application.Common.Abstracts.Caching;
using Architecture.Infrastructure.Caching;
using Architecture.Infrastructure.Caching.RedisSetupConfigurationOptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;

namespace Architecture.WebAPI.Configuration
{
    public class CachingServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexerFactory.GetConnection(configuration));
            services.ConfigureOptions<RedisOptionsSetup>();
            services.ConfigureOptions<RedisConfigurationOptionsSetup>();
            services.ConfigureOptions<CrossCacheEntryOptionSetup>();

            services.AddSingleton<IInMemoryCacheService, InMemoryCacheService>();
            services.AddSingleton<IDistributedCacheService, DistributedCacheService>();
            services.AddSingleton<ICrossCacheService, CrossCacheService>();
            services.AddSingleton<IHostedService, InvalidationCacheChannelSubscriber>();
        }
    }
}
