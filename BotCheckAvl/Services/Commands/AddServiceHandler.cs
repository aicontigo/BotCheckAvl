using System.Threading;
using System.Threading.Tasks;

namespace BotCheckAvl.Services.Commands
{
    public class AddServiceHandler : CommandHandlerBase
    {
        public override BotCommand Command => BotCommand.AddService;

        public override Task<CommandResult> HandleCommand(CancellationToken ct, params string[] arguments)
        {
            var result = new CommandResult
            {
                IsSuccess = true,
                SuccessMessage = "AddService command received"
            };
            return Task.FromResult(result);
        }
    }
}
