using System;

namespace BotCheckAvl.Services.Commands
{
    public static class CommandHandlerFactory
    {
        public static CommandHandlerBase Create(BotCommandEnum command)
        {
            return command switch
            {
                BotCommandEnum.AddService => new AddServiceHandler(),
                BotCommandEnum.DisableService => new DisableServiceHandler(),
                BotCommandEnum.EnableService => new EnableServiceHandler(),
                BotCommandEnum.DeleteService => new DeleteServiceHandler(),
                BotCommandEnum.ShowService => new ShowServiceHandler(),
                BotCommandEnum.ShowAll => new ShowAllHandler(),
                BotCommandEnum.CheckService => new CheckServiceHandler(),
                _ => throw new ArgumentOutOfRangeException(nameof(command), command, null)
            };
        }
    }
}
