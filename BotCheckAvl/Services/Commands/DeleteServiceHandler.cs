using System.Threading;
using System.Threading.Tasks;

namespace BotCheckAvl.Services.Commands
{
    public class DeleteServiceHandler : CommandHandlerBase
    {
        public override BotCommand Command => BotCommand.DeleteService;

        public override Task<CommandResult> HandleCommand(CancellationToken ct, params string[] arguments)
        {
            var result = new CommandResult
            {
                IsSuccess = true,
                SuccessMessage = "DeleteService command received"
            };
            return Task.FromResult(result);
        }
    }
}
