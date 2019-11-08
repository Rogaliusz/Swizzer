using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.Connections.Features;
using Swizzer.Shared.Common.Hubs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Swizzer.Client.Framework;

namespace Swizzer.Client.Web.Api
{
    public abstract class ApiHubWebServiceBase<TCommand, TResponse> : IDisposable
    {
        public abstract string Channel { get; }
        public abstract string Method { get; }

        private readonly IApiHubWebServiceFacade _facade;

        private object _methodRef;
        private HubConnection _hubConnection;

        public ApiHubWebServiceBase(IApiHubWebServiceFacade facade)
        {
            _facade = facade;
        }

        public Task ActivateAsync()
        {
            var uri = new Uri(_facade.ApiSettings.Address + Channel);

            _hubConnection = new HubConnectionBuilder()
                .WithAutomaticReconnect()
                .WithUrl(uri, opt =>
                {
                    opt.Headers.Add("Authorization", $"Bearer {_facade.ApiSettings.Token}");
                })
                .Build();

            _methodRef = _hubConnection.On<TResponse>(Method, OnRecievedAsync);
            return _hubConnection.StartAsync();
        }

        public abstract Task SendComandAsync(TCommand command);
        public abstract Task OnRecievedAsync(TResponse response);

        public async Task StopAsync()
        {
            await _hubConnection.StopAsync();
        }

        protected Task SendCommandAsyncInternal(TCommand command)
        {            
            return _hubConnection.SendAsync(Method, command);
        }

        protected void Publish<TEvent, TMessage>(TMessage message) 
            where TEvent : SwizzerEventBase<TMessage>, new()
        {
            _facade.EventAggregator.GetEvent<TEvent>().Publish(message);
        }

        protected TDesc MapTo<TDesc>(object source)
        {
            var mapped = _facade.Mapper.MapTo<TDesc>(source);
            return mapped;
        }

        public void Dispose()
        {
            _hubConnection?.DisposeAsync();
        }
    }
}
