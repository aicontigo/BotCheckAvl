using System.Threading;
using System.Threading.Tasks;

namespace BotCheckAvl.Services.Commands
{
    public class ShowServiceHandler : CommandHandlerBase
    {
        public override BotCommand Command => BotCommand.ShowService;

        public override Task<CommandResult> HandleCommand(CancellationToken ct, params string[] arguments)
        {
            var result = new CommandResult
            {
                IsSuccess = true,
                SuccessMessage = "ShowService command received"
            };
            return Task.FromResult(result);
        }
    }
}
