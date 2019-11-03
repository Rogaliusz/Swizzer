using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swizzer.Web.Infrastructure.Mappers
{
    public static class AutoMapperConfiguration
    {
        public static IMapper Initialize()
        {
            return new MapperConfiguration(cfg =>
            {

                cfg.AddMaps(typeof(AutoMapperConfiguration).Assembly);

            }).CreateMapper();
        }
    }
}
