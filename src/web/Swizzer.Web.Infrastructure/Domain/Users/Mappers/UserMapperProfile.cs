using Swizzer.Shared.Common.Domain.Users.Dto;
using Swizzer.Web.Infrastructure.Domain.Users.Models;
using Swizzer.Web.Infrastructure.Mappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swizzer.Web.Infrastructure.Domain.Users.Mappers
{
    public class UserMapperProfile : SwizzerMapperProfile
    {
        public UserMapperProfile()
        {
            CreateMap<User, UserDto>()
                .ReverseMap();
        }
    }
}
