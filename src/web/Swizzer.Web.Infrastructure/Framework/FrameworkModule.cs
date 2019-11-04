using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Swizzer.Shared.Common.Extensions;
using Swizzer.Web.Infrastructure.Framework.Caching;
using Swizzer.Web.Infrastructure.Framework.Security;
using Swizzer.Web.Infrastructure.Mappers;
using Swizzer.Web.Infrastructure.Services;

namespace Swizzer.Web.Infrastructure.Framework
{
    public class FrameworkModule : Autofac.Module
    {
        private readonly IConfiguration _configuration;

        public FrameworkModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var cacheSettings = _configuration.CreateSettings<CacheSettings>();
            var securitySettings = _configuration.CreateSettings<SecuritySettings>();

            builder.RegisterInstance(cacheSettings)
                .SingleInstance();

            builder.RegisterInstance(securitySettings)
                .SingleInstance();

            builder.RegisterType<MemoryCache>()
                .As<IMemoryCache>()
                .SingleInstance();

            builder.RegisterType<CacheService>()
                .As<ICacheService>()
                .SingleInstance();

            builder.RegisterType<SecurityService>()
                .As<ISecurityService>()
                .InstancePerLifetimeScope();
        }
    }
}
