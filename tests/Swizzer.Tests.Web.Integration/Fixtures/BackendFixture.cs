using Autofac.Extensions.DependencyInjection;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using Swizzer.Shared.Common.Framework;
using Swizzer.Web.Api;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Swizzer.Shared.Common.Extensions;
using Swizzer.Shared.Common.Domain.Users.Commands;
using Swizzer.Shared.Common.Domain.Users.Dto;
using Microsoft.AspNetCore.SignalR.Client;
using Swizzer.Shared.Common.Hubs;

namespace Swizzer.Tests.Web.Integration.Fixtures
{
    public class BackendFixture
    {
        public HttpClient Client { get; }
        private WebApplicationFactory<Startup> _webApplicationFactory;
        private string _bearerFormat = "Bearer {0}";
        private string _token;

        public BackendFixture()
        {
            _webApplicationFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(webHost =>
            {
                webHost.ConfigureServices(x => x.AddAutofac());
            });

            Client = _webApplicationFactory.CreateClient();
        }

        public async Task LoginAsync(string username, string password)
        {
            var loginCmd = new UserLoginCommand
            {
                Id = Guid.NewGuid(),
                Email = username,
                Password = password
            };

            var jwt = await SendAsync<JwtDto>(HttpMethod.Post, "api/users/login", loginCmd);
            _token = jwt.Token;
        }

        public async Task<TResponse> SendAsync<TResponse>(HttpMethod method, string url, object body = null)
        {
            var request = new HttpRequestMessage();
            request.Method = method;
            request.RequestUri = new Uri(Client.BaseAddress, url);

            if (!_token.IsEmpty())
            {
                Client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _token);
            }

            if (body != null)
            {
                var json = SwizzerJsonSerializer.Serialize(body);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }

            var httpResponseMessage = await Client.SendAsync(request);
            var responseString = await httpResponseMessage.Content.ReadAsStringAsync();
            httpResponseMessage.IsSuccessStatusCode.Should().BeTrue(responseString);

            return SwizzerJsonSerializer.Deserialize<TResponse>(responseString);
        }

        public HubConnection CreateHubConnection()
        {
            var connection = new HubConnectionBuilder()
              
              .WithUrl(new Uri(Client.BaseAddress, Channels.ChatChannel), opt =>
              {
                  opt.Headers.Add("Authorization", "Bearer " + _token);
                  opt.HttpMessageHandlerFactory = _ => _webApplicationFactory.Server.CreateHandler();
              })
              .Build();

            return connection;
        }
    }
}
