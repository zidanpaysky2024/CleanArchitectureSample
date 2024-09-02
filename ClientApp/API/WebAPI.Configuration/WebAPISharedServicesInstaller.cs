using Architecture.Application.Common.Abstracts.Business;
using Architecture.Application.Common.Abstracts.Persistence;
using Architecture.Application.Common.Messaging;
using Architecture.Application.Common.Exceptions;
using Common.DependencyInjection.Extensions;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Architecture.WebAPI.Configuration
{
    public class WebApiSharedServicesInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddValidatorsFromAssembly(GetAssembly(nameof(Application)));
            services.AddAutoMapper(GetAssembly(nameof(Application)));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(GetAssembly(nameof(Application))));
            services.RegisterAllChildsDynamic(ServiceLifetime.Transient, nameof(Application), nameof(Persistence.EF), typeof(IEntitySet<>));
            services.RegisterAllForBaseDynamic(ServiceLifetime.Transient, nameof(Application), typeof(IRequestResponsePipeline<,>));
            services.RegisterAllForBaseDynamic(ServiceLifetime.Transient, nameof(Application), typeof(IRequestPreProcessor<>));
            services.RegisterAllForBaseDynamic(ServiceLifetime.Transient, nameof(Application), typeof(IRequestPostProcessor<>));
            services.RegisterAllChildsDynamic(ServiceLifetime.Scoped, nameof(Application), typeof(IService));

            static Assembly GetAssembly(string assemblyName) => Array.Find(AppDomain.CurrentDomain.GetAssemblies(), a => a.FullName != null && a.FullName.Contains(assemblyName))
                                            ?? throw new NotFoundAssmblyException(assemblyName);
        }
    }
}
