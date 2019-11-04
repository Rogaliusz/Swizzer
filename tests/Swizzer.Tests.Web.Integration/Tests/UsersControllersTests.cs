using FluentAssertions;
using Swizzer.Shared.Common.Domain.Users.Commands;
using Swizzer.Shared.Common.Domain.Users.Dto;
using Swizzer.Shared.Common.Dto;
using Swizzer.Shared.Common.Framework;
using Swizzer.Tests.Web.Integration.Fixtures;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Swizzer.Tests.Web.Integration.Tests
{
    public class UsersControllersTests : IClassFixture<BackendFixture>
    {
        private readonly BackendFixture _backendFixture;

        public UsersControllersTests(BackendFixture backendFixture)
        {
            _backendFixture = backendFixture;
        }

        [Fact]
        public async Task Users_AfterRegister_Should_LoginAndGetOwnProfile()
        {
            var command = new UserRegisterCommand
            {
                Id = Guid.NewGuid(),
                Email = "sample@net.pl",
                Password = "current_password",
                Name = "Mirrabel",
                Surname = "Ernest"
            };

            await _backendFixture.SendAsync<UserDto>(HttpMethod.Post, "api/users", command);
            await _backendFixture.LoginAsync(command.Email, command.Password);
            var user = await _backendFixture.SendAsync<UserDto>(HttpMethod.Get, $"api/users/{command.Id}");

            user.Id.Should().Be(command.Id);
            user.Name.Should().Be(command.Name);
            user.Surname.Should().Be(command.Surname);
            user.Email.Should().Be(command.Email);
            user.CreatedAt.Should().BeAfter(DateTime.UtcNow - TimeSpan.FromDays(1));
        }
    }
}
