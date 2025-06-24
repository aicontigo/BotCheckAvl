using System.Threading;
using System.Threading.Tasks;

namespace BotCheckAvl.Services.Commands
{
    public class ShowAllHandler : CommandHandlerBase
    {
        public override BotCommand Command => BotCommand.ShowAll;

        public override Task<CommandResult> HandleCommand(CancellationToken ct, params string[] arguments)
        {
            var result = new CommandResult
            {
                IsSuccess = true,
                SuccessMessage = "ShowAll command received"
            };
            return Task.FromResult(result);
        }
    }
}
