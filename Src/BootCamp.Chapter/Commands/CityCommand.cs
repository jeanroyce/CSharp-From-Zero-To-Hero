using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using BootCamp.Chapter.Models;
using CliFx.Attributes;
using MoreLinq;
using Newtonsoft.Json;

namespace BootCamp.Chapter.Commands
{
    public enum TempEnum
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
        // TODO: Rename variable
        [CommandOption("temp", 't', IsRequired = true)]
        public TempEnum Temp { get; set; }
        
        // TODO: Rename variable
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
            var transactionDatabase = transactions
                .GroupBy(n => n.City, (city, values) =>
                {
                    var transaction = values.ToList();
                    return new CityByItems(city, transaction.Select(n => n.Price).Count());
                });
            
            var result = Temp switch
            {
                TempEnum.Max => transactionDatabase.MaxBy(n => n.Count),
                TempEnum.Min => transactionDatabase.MinBy(n => n.Count),
                _ => throw new InvalidEnumArgumentException()
            };
            
            return result.Select(n=> n.City);
        }
        
        private IEnumerable<string> GetCityNameByMoney(IEnumerable<Transaction> transactions)
        {
            var transactionDatabase = transactions
                .GroupBy(n => n.City, (city, values) =>
                {
                    var transaction = values.ToList();
                    return new CityByMoney(city, transaction.Select(n => n.Price).Sum());
                });
            
            var result = Temp switch
            {
                TempEnum.Max => transactionDatabase.MaxBy(n => n.Money),
                TempEnum.Min => transactionDatabase.MinBy(n => n.Money),
                _ => throw new InvalidEnumArgumentException()
            };
            
            return result.Select(n=> n.City);
        }
    }
}
