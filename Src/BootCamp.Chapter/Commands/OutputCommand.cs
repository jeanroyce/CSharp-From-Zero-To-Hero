using System.IO;
using System.Threading.Tasks;
using CliFx;
using CliFx.Attributes;

namespace BootCamp.Chapter.Commands
{
    public abstract class OutputCommand : ICommand
    {
        [CommandParameter(0, Description = "File to read.")]
        public string FilePath { get; set; }
        
        [CommandOption("output", 'o', IsRequired = true)]
        public string OutputPath { get; set; }
        
        public ValueTask ExecuteAsync(IConsole console)
        {
            var json = ProcessCommand();
            File.WriteAllText(OutputPath, json);
            return default;
        }
        
        private protected abstract string ProcessCommand();
    }
}
