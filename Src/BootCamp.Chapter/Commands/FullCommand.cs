using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BootCamp.Chapter.Models;
using CliFx;
using CliFx.Attributes;
using Newtonsoft.Json;

namespace BootCamp.Chapter.Commands
{
    [Command("full")]
    public class FullCommand : ICommand
    {
        [CommandParameter(0, Description = "File to read.")]
        public string FilePath { get; set; }

        public ValueTask ExecuteAsync(IConsole console)
        {
            ProcessCommand();
            return default;
        }

        private void ProcessCommand()
        {
            var transactions = JsonReader.Read(FilePath);

            var transactionDatabase = transactions.GroupBy(n => n.Shop).Select(n => n.ToList());

            foreach (var transaction in transactionDatabase) GetShopSummary(transaction);
        }

        private static void GetShopSummary(List<Transaction> shop)
        {
            var storeName = shop.FirstOrDefault()?.Shop;
            var summaries = shop.Select(n => new TransactionSummary(n.City, n.Street, n.Item, n.DateTime, n.Price));

            if (storeName == null) return;
            var json = JsonConvert.SerializeObject(new {Shop = storeName, Transactions = summaries},
                Formatting.Indented);
            File.WriteAllText($"{storeName}.json", json);
        }
    }
}
