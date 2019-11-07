using Prism.Events;
using Swizzer.Client.Cqrs;
using Swizzer.Client.Domain.Users.Events;
using Swizzer.Client.Mapper;
using Swizzer.Client.Web.Api;
using Swizzer.Shared.Common;
using Swizzer.Shared.Common.Domain.Users.Commands;
using Swizzer.Shared.Common.Domain.Users.Dto;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Swizzer.Client.Domain.Users
{
    public class UserCommandHandler :
        ICommandHandler<UserRegisterCommand>,
        ICommandHandler<UserLoginCommand>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ICurrentUserContext _currentUserContext;
        private readonly ISwizzerMapper _mapper;
        private readonly ApiSettings _apiSettings;
        private readonly ApiHttpWebService _apiHttpWebService;

        public UserCommandHandler(
            IEventAggregator eventAggregator,
            ICurrentUserContext currentUserContext,
            ISwizzerMapper mapper,
            ApiSettings apiSettings,
            ApiHttpWebService apiHttpWebService)
        {
            this._eventAggregator = eventAggregator;
            this._currentUserContext = currentUserContext;
            this._mapper = mapper;
            this._apiSettings = apiSettings;
            this._apiHttpWebService = apiHttpWebService;
        }

        public async Task HandleAsync(UserRegisterCommand command)
        {
            await _apiHttpWebService.SendAsync<object>(HttpMethod.Post, Routes.Users.Main, command);
            await HandleAsync(_mapper.MapTo<UserLoginCommand>(command));
        }

        public async Task HandleAsync(UserLoginCommand command)
        {
            var jwtDto = await _apiHttpWebService.SendAsync<JwtDto>(HttpMethod.Post, Routes.Users.Login, command);

            _apiSettings.Token = jwtDto.Token;
            _currentUserContext.CurrentUser = jwtDto.User;

            _eventAggregator.GetEvent<UserLoggedEvent>().Publish();
        }
    }
}
