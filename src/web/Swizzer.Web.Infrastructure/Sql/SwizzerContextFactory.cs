using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Swizzer.Shared.Common.Extensions;
using Swizzer.Web.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using Swizzer.Web.Infrastructure.Framework.Extensions;

namespace Swizzer.Web.Infrastructure.Sql
{
    public class SwizzerContextFactory : IDesignTimeDbContextFactory<SwizzerContext>
    {
        private readonly SqlSettings _sqlSettings;

        public SwizzerContextFactory()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            _sqlSettings = configuration.CreateSettings<SqlSettings>();
        }

        public SwizzerContext CreateDbContext(string[] args)
        {
            return new SwizzerContext(_sqlSettings);
        }
    }
}
