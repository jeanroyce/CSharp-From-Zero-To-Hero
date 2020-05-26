using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BootCamp.Chapter.Models;
using CliFx.Attributes;
using Newtonsoft.Json;

namespace BootCamp.Chapter.Commands.Output
{
    [Command("daily")]
    public class DailyCommand : OutputCommand
    {
        [CommandOption("shop", 's', Description = "Shop Name", IsRequired = true)]
        public string ShopName { get; set; }

        private protected override string ProcessCommand()
        {
            Console.WriteLine(ShopName);
            var transactions = JsonReader.Read(FilePath);

            var shopDailySummaries = CheckShopDailyByName(transactions);
            return JsonConvert.SerializeObject(shopDailySummaries, Formatting.Indented);
        }

        private IEnumerable<DailySummary> CheckShopDailyByName(IEnumerable<Transaction> transactions)
        {
            var transactionDatabase = transactions.Where(n => n.Shop.Equals(ShopName))
                .GroupBy(n => n.DateTime.Day, (date, values) =>
                {
                    var transaction = values.ToList();
                    return new DailySummary(
                        transaction.First().DateTime.ToString("dddd", CultureInfo.GetCultureInfo("en-US")),
                        transaction.Select(n => n.Price).Sum());
                });

            return transactionDatabase;
        }
    }
}
