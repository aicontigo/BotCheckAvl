using System.Threading;
using System.Threading.Tasks;

namespace BotCheckAvl.Services.Commands
{
    public abstract class CommandHandlerBase
    {
        public abstract BotCommandEnum Command { get; }

        public abstract Task<CommandResult> HandleCommand(CancellationToken ct, params string[] arguments);
    }
}
