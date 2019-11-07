using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swizzer.Client.Mapper
{
    public class SwizzerMapperConfiguration
    {
        public static IMapper Initialize()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(SwizzerMapperConfiguration).Assembly);
            }).CreateMapper();
        }
    }
}
