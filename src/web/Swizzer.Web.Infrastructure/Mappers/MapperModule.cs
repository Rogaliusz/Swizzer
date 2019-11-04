using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace Swizzer.Web.Infrastructure.Mappers
{
    public class MapperModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterInstance(AutoMapperConfiguration.Initialize())
                .SingleInstance();

            builder.RegisterType<SwizzerMapper>()
                .As<ISwizzerMapper>()
                .SingleInstance();
        }
    }
}
