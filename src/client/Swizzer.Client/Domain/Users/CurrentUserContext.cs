using Swizzer.Shared.Common.Domain.Users.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swizzer.Client.Domain.Users
{
    public interface ICurrentUserContext
    {
        UserDto CurrentUser { get; set; }
    }

    public class CurrentUserContext : ICurrentUserContext
    {
        public UserDto CurrentUser { get; set; }
    }
}
