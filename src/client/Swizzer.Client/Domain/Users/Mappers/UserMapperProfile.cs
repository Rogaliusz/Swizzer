using Swizzer.Client.Mapper;
using Swizzer.Client.ViewModels;
using Swizzer.Shared.Common.Domain.Users.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swizzer.Client.Domain.Users.Mappers
{
    public class UserMapperProfile : SwizzerMapperProfile
    {
        public UserMapperProfile()
        {
            CreateMap<RegisterViewModel, UserRegisterCommand>();
            CreateMap<LoginViewModel, UserLoginCommand>();
            CreateMap<UserRegisterCommand, UserLoginCommand>();
        }
    }
}
