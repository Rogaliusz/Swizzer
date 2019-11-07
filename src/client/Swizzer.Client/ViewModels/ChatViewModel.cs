using System;
using System.Collections.Generic;
using System.Text;
using Swizzer.Client.Cqrs;
using Swizzer.Client.Cqrs.Queries;
using Swizzer.Client.Domain.Users;
using Swizzer.Client.Mapper;

namespace Swizzer.Client.ViewModels
{
    public class ChatViewModel : ViewModelBase
    {
        private string _userName;

        public string UserName { get => _userName; set => SetProperty(ref _userName, value); }

        public ChatViewModel(ICurrentUserContext currentUserContext,
            IQueryDispatcher queryDispatcher, 
            ICommandDispatcher commandDispatcher, 
            ISwizzerMapper swizzerMapper) : base(queryDispatcher, commandDispatcher, swizzerMapper)
        {

        }

    }
}
