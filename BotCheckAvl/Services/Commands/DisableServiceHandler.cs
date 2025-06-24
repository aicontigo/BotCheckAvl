using System.Threading;
using System.Threading.Tasks;

namespace BotCheckAvl.Services.Commands
{
    public class DisableServiceHandler : CommandHandlerBase
    {
        public override BotCommandEnum Command => BotCommandEnum.DisableService;

        public override Task<CommandResult> HandleCommand(CancellationToken ct, params string[] arguments)
        {
            var result = new CommandResult
            {
                IsSuccess = true,
                SuccessMessage = "DisableService command received"
            };
            return Task.FromResult(result);
        }
    }
}
