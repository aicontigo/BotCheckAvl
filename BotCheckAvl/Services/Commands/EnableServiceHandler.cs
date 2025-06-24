using System.Threading;
using System.Threading.Tasks;

namespace BotCheckAvl.Services.Commands
{
    public class EnableServiceHandler : CommandHandlerBase
    {
        public override BotCommand Command => BotCommand.EnableService;

        public override Task<CommandResult> HandleCommand(CancellationToken ct, params string[] arguments)
        {
            var result = new CommandResult
            {
                IsSuccess = true,
                SuccessMessage = "EnableService command received"
            };
            return Task.FromResult(result);
        }
    }
}
