using System;

namespace BotCheckAvl.Services.Commands
{
    public static class CommandHandlerFactory
    {
        public static CommandHandlerBase Create(BotCommand command)
        {
            return command switch
            {
                BotCommand.AddService => new AddServiceHandler(),
                BotCommand.DisableService => new DisableServiceHandler(),
                BotCommand.EnableService => new EnableServiceHandler(),
                BotCommand.DeleteService => new DeleteServiceHandler(),
                BotCommand.ShowService => new ShowServiceHandler(),
                BotCommand.ShowAll => new ShowAllHandler(),
                BotCommand.CheckService => new CheckServiceHandler(),
                _ => throw new ArgumentOutOfRangeException(nameof(command), command, null)
            };
        }
    }
}
