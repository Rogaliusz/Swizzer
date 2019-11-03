using Autofac;
using Microsoft.Extensions.Configuration;
using Swizzer.Shared.Common.Extensions;
using Swizzer.Web.Infrastructure.Settings;
using Swizzer.Web.Infrastructure.Sql;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swizzer.Web.Infrastructure.IoC.Modules
{
    public class SqlModule : Autofac.Module
    {
        private readonly IConfiguration _configuration;

        public SqlModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<SwizzerContext>()
                .InstancePerLifetimeScope();

            var sqlSettings = _configuration.CreateSettings<SqlSettings>();

            builder.RegisterInstance(sqlSettings)
                .SingleInstance();
        }
    }
}
