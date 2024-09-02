﻿using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Architecture.WebAPI.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection InstallServices(this IServiceCollection services,
                                                         IConfiguration configuration,
                                                         params Assembly[] assemblies)
        {
            IEnumerable<IServiceInstaller> serviceInstallers = assemblies
                 .SelectMany(a => a.DefinedTypes)
                 .Where(IsAssignableToType<IServiceInstaller>)
                 .Select(Activator.CreateInstance)
                 .Cast<IServiceInstaller>();

            foreach (var serviceInstaller in serviceInstallers)
            {
                serviceInstaller.Install(services, configuration);
            }

            return services;

            static bool IsAssignableToType<T>(TypeInfo typeInfo) =>
                typeof(T).IsAssignableFrom(typeInfo)
                && !typeInfo.IsInterface
                && !typeInfo.IsAbstract;
        }
    }
}
