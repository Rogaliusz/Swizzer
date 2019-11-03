using System;
using System.Collections.Generic;
using System.Text;

namespace Swizzer.Shared.Common.Dto
{
    public class PaginationDto<TDto>
    {
        public ICollection<TDto> Data { get; set; }
        public int TotalCount { get; set; }
    }
}
