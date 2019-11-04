using FluentAssertions;
using Swizzer.Shared.Common.Domain.Messages.Commands;
using Swizzer.Shared.Common.Domain.Users.Commands;
using Swizzer.Shared.Common.Domain.Users.Dto;
using Swizzer.Shared.Common.Dto;
using Swizzer.Shared.Common.Framework;
using Swizzer.Shared.Common.Hubs;
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
        [Fact]
        public async Task Users_Should_SendMessages()
        {
            var command = new UserRegisterCommand
            {
                Id = Guid.NewGuid(),
                Email = "sample@net.pl",
                Password = "current_password",
                Name = "Mirrabel",
                Surname = "Ernest"
            };

            var command2 = new UserRegisterCommand
            {
                Id = new Guid("6759d0df-9838-4641-b332-f251b05fcd44"),
                Email = "sample2@net.pl",
                Password = "current_password",
                Name = "Mirrabel",
                Surname = "Ernest"
            };

           // await _backendFixture.SendAsync<UserDto>(HttpMethod.Post, "api/users", command2);

            await _backendFixture.LoginAsync(command.Email, command.Password);

            var connection1 = _backendFixture.CreateHubConnection();
            var message = new CreateMessageCommand
            {
                Id = Guid.NewGuid(),
                Content = "Mario bross",
                Reciever = new Guid("6759d0df-9838-4641-b332-f251b05fcd44")
            };

            await _backendFixture.LoginAsync(command2.Email, command2.Password);
            var connection2 = _backendFixture.CreateHubConnection();

            await connection2.StartAsync();

            var tcs = new TaskCompletionSource<CreateMessageCommand>();

            connection2.On(Channels.Chat.Messages, new[] { typeof(CreateMessageCommand)}, async (p1, p2) =>
            {
                
                tcs.SetResult(p1[0] as CreateMessageCommand);
            },
            new object());

            await connection1.StartAsync();
            await connection1.SendCoreAsync(Channels.Chat.Messages, new[] { message });

            await tcs.Task;

            tcs.Task.Result.Id.Should().Be(message.Id);
            tcs.Task.Result.Content.Should().Be(message.Content);
            tcs.Task.Result.Reciever.Should().Be(message.Reciever);
        }
    }
}
