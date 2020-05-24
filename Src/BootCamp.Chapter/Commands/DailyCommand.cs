using System;
using CliFx.Attributes;

namespace BootCamp.Chapter.Commands
{
    [Command("daily")]
    public class DailyCommand : OutputCommand
    {
        [CommandOption("shop", 's', Description = "Shop Name", IsRequired = true)]
        public string ShopName { get; set; }

        private protected override string ProcessCommand()
        {
            Console.WriteLine(ShopName);
            throw new NotImplementedException();
        }
    }
}
