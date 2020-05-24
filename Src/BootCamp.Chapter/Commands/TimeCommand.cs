using System;
using CliFx.Attributes;

namespace BootCamp.Chapter.Commands
{
    [Command("time")]
    public class TimeCommand : OutputCommand
    {
        [CommandOption("interval", 'i', Description = "eg. 14:00-22:00", IsRequired = true)]
        public string TimeInterval { get; set; }

        private protected override string ProcessCommand()
        {
            Console.WriteLine(TimeInterval);
            throw new NotImplementedException();
        }
    }
}
