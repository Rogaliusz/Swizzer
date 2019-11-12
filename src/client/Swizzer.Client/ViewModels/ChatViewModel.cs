using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using Prism.Events;
using Swizzer.Client.Cqrs;
using Swizzer.Client.Cqrs.Queries;
using Swizzer.Client.Domain.Messages.Events;
using Swizzer.Client.Domain.Users;
using Swizzer.Client.Mapper;
using Swizzer.Client.Web.Api;
using Swizzer.Shared.Common.Domain.Messages.Commands;
using Swizzer.Shared.Common.Domain.Messages.Dto;
using Swizzer.Shared.Common.Domain.Messages.Queries;
using Swizzer.Shared.Common.Domain.Users.Dto;
using Swizzer.Shared.Common.Domain.Users.Queries;
using Swizzer.Shared.Common.Dto;

namespace Swizzer.Client.ViewModels
{
    public class ChatViewModel : ViewModelBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly SubscriptionToken _recievedMessageToken;
        private readonly MessageApiHubWebService _messageApiHubWebService;
        private string _userName;
        private string _messageContent;
        private UserDto _currentReciever;
        private ICurrentUserContext _currentUserContext;

        public string MessageContent { get => _messageContent; set => SetProperty(ref _messageContent, value); }
        public string UserName { get => _userName; set => SetProperty(ref _userName, value); }
        public UserDto CurrentReciever
        {
            get => _currentReciever; 
            set
            {
                if (SetProperty(ref _currentReciever, value))
                {
                    GetUserMessages();
                }
            }
        }

        public ObservableCollection<UserDto> Users { get; set; }
        public ObservableCollection<MessageDto> Messages { get;set; }

        public ICommand GetUserMessagesCommand { get; private set; }
        public ICommand SendMessageCommand { get; private set; }

        public ChatViewModel(
            MessageApiHubWebService messageApiHubWebService,
            IEventAggregator eventAggregator,
            ICurrentUserContext currentUserContext,
            IViewModelFacade viewModelFacade) : base(viewModelFacade)
        {
            Users = new ObservableCollection<UserDto>();
            Messages = new ObservableCollection<MessageDto>();

            UserName = $"{currentUserContext.CurrentUser.Name} {currentUserContext.CurrentUser.Surname}";

            _messageApiHubWebService = messageApiHubWebService;
            _currentUserContext = currentUserContext;
            _eventAggregator = eventAggregator;
            _recievedMessageToken = eventAggregator.GetEvent<MessageRecievedEvent>().Subscribe(MessageRecievedHandle);

            SendMessageCommand = new DelegateCommand(SendMessage, CanSendMessage);
            GetUserMessagesCommand = new DelegateCommand(GetUserMessages, CanGetUserMessages);
        }

        private async void SendMessage()
        {
            var command = MapTo<CreateMessageCommand>(this);
            await DispatchCommandAsync(command);
        }

        private bool CanSendMessage()
            => true;

        private async void GetUserMessages()
        {
            Messages.Clear();

            var query = new GetMessagesQuery
            {
                Reciever = CurrentReciever.Id,
                OrderBy = $"{nameof(MessageDto.CreatedAt)}-desc"
            };

            var messages = await DispatchQueryAsync<GetMessagesQuery, PaginationDto<MessageDto>>(query);
            foreach (var message in messages.Data)
            {
                FormatMessage(message);
                Messages.Add(message);
            }
        }

        private bool CanGetUserMessages()
            => true;

        private void MessageRecievedHandle(MessageDto obj)
        {
            FormatMessage(obj);

            Messages.Add(obj);
        }

        private void FormatMessage(MessageDto obj)
        {
            if (obj.ReceiverId == CurrentReciever?.Id)
            {
                obj.Receiver = CurrentReciever;
                obj.Recipient = _currentUserContext.CurrentUser;
            }
            else if (obj.RecipientId == CurrentReciever?.Id)
            {
                obj.Recipient = CurrentReciever;
                obj.Receiver = _currentUserContext.CurrentUser;
            }
        }

        public override async Task InitializeAsync(object parameter = null)
        {
            await base.InitializeAsync(parameter);

            var query = new GetUsersQuery();
            var users = await DispatchQueryAsync<GetUsersQuery, PaginationDto<UserDto>>(query);

            foreach (var user in users.Data)
            {
                Users.Add(user);
            }

            await _messageApiHubWebService.ActivateAsync();
        }
    }
}
