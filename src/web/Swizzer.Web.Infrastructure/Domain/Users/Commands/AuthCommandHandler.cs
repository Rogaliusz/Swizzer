using Microsoft.EntityFrameworkCore;
using Swizzer.Shared.Common.Domain.Users.Commands;
using Swizzer.Shared.Common.Domain.Users.Dto;
using Swizzer.Shared.Common.Exceptions;
using Swizzer.Web.Infrastructure.Cqrs.Commands;
using Swizzer.Web.Infrastructure.Domain.Users.Models;
using Swizzer.Web.Infrastructure.Framework;
using Swizzer.Web.Infrastructure.Mappers;
using Swizzer.Web.Infrastructure.Services;
using Swizzer.Web.Infrastructure.Sql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Swizzer.Web.Infrastructure.Domain.Users.Commands
{
    public class AuthCommandHandler : ICommandHandler<UserRegisterCommand>,
        ICommandHandler<UserLoginCommand>
    {
        private readonly ICacheService _cacheService;
        private readonly ISwizzerMapper _mapper;
        private readonly ISecurityService _securityService;
        private readonly SwizzerContext _swizzerContext;

        public AuthCommandHandler(
            ICacheService cacheService,
            ISwizzerMapper mapper,
            ISecurityService securityService,
            SwizzerContext swizzerContext)
        {
            _cacheService = cacheService;
            _mapper = mapper;
            _securityService = securityService;
            _swizzerContext = swizzerContext;
        }

        public async Task HandleAsync(UserRegisterCommand command)
        {
            var user = await _swizzerContext.Users.FirstOrDefaultAsync(x => x.Email == command.Email);
            if (user != null)
            {
                throw new SwizzerServerException(ServerErrorCodes.InvalidParamter, $"User with email {command.Email} already exists");
            }

            user = _mapper.MapTo<UserRegisterCommand, User>(command);
            user.Salt = _securityService.GetSalt();
            user.Hash = _securityService.GetHash(command.Password, user.Salt);

            await _swizzerContext.AddAsync(user);
        }

        public async Task HandleAsync(UserLoginCommand command)
        {
            var user = await _swizzerContext.Users.FirstOrDefaultAsync(x => x.Email == command.Email);
            if (user == null)
            {
                throw new SwizzerServerException(ServerErrorCodes.InvalidParamter, $"User with email {command.Email} not exists");
            }

            var currentHash = _securityService.GetHash(command.Password, user.Salt);
            if (currentHash != user.Hash)
            {
                throw new SwizzerServerException(ServerErrorCodes.UnauthorizedAccess);
            }

            var userDto = _mapper.MapTo<UserDto>(user);
            var jwt = _securityService.GetJwt(userDto);

            jwt.Id = command.Id;
            _cacheService.Set(jwt);
        }
    }
}
