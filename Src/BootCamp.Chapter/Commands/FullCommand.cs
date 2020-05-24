using System.Threading.Tasks;
using CliFx;
using CliFx.Attributes;

namespace BootCamp.Chapter.Commands
{
    [Command("full")]
    public class FullCommand : ICommand
    {
        public ValueTask ExecuteAsync(IConsole console)
        {
            return default;
        }
    }
}
