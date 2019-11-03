using Swizzer.Shared.Common.Providers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swizzer.Shared.Common.Domain.Users.Dto
{
    public class UserDto : IDtoProvider,
        IIdProvider,
        ICreatedAtProvider,
        INameProvider
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
