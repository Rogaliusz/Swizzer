using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swizzer.Web.Infrastructure.Mappers
{
    public  interface ISwizzerMapper
    {
        TDesc MapTo<TDesc>(object source);
        TDesc MapTo<TSrc, TDesc>(TSrc source);
    }

    public class SwizzerMapper : ISwizzerMapper
    {
        private readonly IMapper _mapper;

        public SwizzerMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TDesc MapTo<TSrc, TDesc>(TSrc source)
            => _mapper.Map<TDesc>(source);

        public TDesc MapTo<TDesc>(object source)
            => _mapper.Map<TDesc>(source);
    }
}
