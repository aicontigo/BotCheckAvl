using System.Threading;
using System.Threading.Tasks;

namespace BotCheckAvl.Services.Commands
{
    public class ShowAllHandler : CommandHandlerBase
    {
        public override BotCommandEnum Command => BotCommandEnum.ShowAll;

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
