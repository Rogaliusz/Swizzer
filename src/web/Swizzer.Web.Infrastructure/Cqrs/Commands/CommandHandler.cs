using Swizzer.Shared.Common.Providers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Swizzer.Web.Infrastructure.Cqrs.Commands
{
    public interface ICommandHandler<TCommand>
        where TCommand : ICommandProvider
    {
        Task HandleAsync(TCommand command); 
    }
}
