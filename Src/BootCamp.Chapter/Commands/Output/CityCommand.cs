using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using BootCamp.Chapter.Models;
using CliFx.Attributes;
using MoreLinq;
using Newtonsoft.Json;

namespace BootCamp.Chapter.Commands.Output
{
    public enum MaxOrMin
    {
        Max, Min
    }

    public enum CriteriaEnum
    {
        Items, Money
    }

    [Command("city")]
    public class CityCommand : OutputCommand
    {
        // TODO: Better command/variable name
        [CommandOption("maxormin", 'm', IsRequired = true)]
        public MaxOrMin MaxOrMin { get; set; }

        // TODO: Better command/variable name
        [CommandOption("criteria", 'c', IsRequired = true)]
        public CriteriaEnum Criteria { get; set; }

        private protected override string ProcessCommand()
        {
            var transactions = JsonReader.Read(FilePath);
            IEnumerable<string> result = Criteria switch
            {
                CriteriaEnum.Items => GetCityNameByItems(transactions),
                CriteriaEnum.Money => GetCityNameByMoney(transactions),
                _ => throw new InvalidEnumArgumentException()
            };

            return JsonConvert.SerializeObject(new {city = result}, Formatting.Indented);
        }

        private IEnumerable<string> GetCityNameByItems(IEnumerable<Transaction> transactions)
        {
            var transactionDatabase = transactions.GroupBy(n => n.City, (city, values) =>
            {
                var transaction = values.ToList();
                return new CityByItems(city, transaction.Select(n => n.Price).Count());
            });

            var result = MaxOrMin switch
            {
                MaxOrMin.Max => transactionDatabase.MaxBy(n => n.Count),
                MaxOrMin.Min => transactionDatabase.MinBy(n => n.Count),
                _ => throw new InvalidEnumArgumentException()
            };

            return result.Select(n => n.City);
        }

        private IEnumerable<string> GetCityNameByMoney(IEnumerable<Transaction> transactions)
        {
            var transactionDatabase = transactions.GroupBy(n => n.City, (city, values) =>
            {
                var transaction = values.ToList();
                return new CityByMoney(city, transaction.Select(n => n.Price).Sum());
            });

            var result = MaxOrMin switch
            {
                MaxOrMin.Max => transactionDatabase.MaxBy(n => n.Money),
                MaxOrMin.Min => transactionDatabase.MinBy(n => n.Money),
                _ => throw new InvalidEnumArgumentException()
            };

            return result.Select(n => n.City);
        }
    }
}
