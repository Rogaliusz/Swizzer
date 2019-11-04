using AutoMapper;
using Swizzer.Shared.Common.Dto;
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

                cfg.CreateMap(typeof(PaginationDto<>), typeof(PaginationDto<>));

            }).CreateMapper();
        }
    }
}
