using System.Collections.Generic;

namespace BotCheckAvl.Services.Commands
{
    public class CommandResult
    {
        public bool IsSuccess { get; set; }
        public List<string> Errors { get; } = new();
        public List<string> Warnings { get; } = new();
        public string? SuccessMessage { get; set; }
    }
}
