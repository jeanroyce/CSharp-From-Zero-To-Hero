using System;
using System.Threading.Tasks;
using CliFx;

namespace BootCamp.Chapter
{
    public static class Program
    {
        public static async Task<int> Main(string[] args) =>
            await new CliApplicationBuilder()
                .AddCommandsFromThisAssembly()
                .Build()
                .RunAsync();
    }
}
