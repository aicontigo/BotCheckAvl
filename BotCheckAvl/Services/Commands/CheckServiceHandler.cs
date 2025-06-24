using System.Threading;
using System.Threading.Tasks;

namespace BotCheckAvl.Services.Commands
{
    public class CheckServiceHandler : CommandHandlerBase
    {
        public override BotCommand Command => BotCommand.CheckService;

        public override Task<CommandResult> HandleCommand(CancellationToken ct, params string[] arguments)
        {
            var result = new CommandResult
            {
                IsSuccess = true,
                SuccessMessage = "CheckService command received"
            };
            return Task.FromResult(result);
        }
    }
}
