using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BootCamp.Chapter.Exceptions;
using BootCamp.Chapter.Models;
using CliFx.Attributes;
using MoreLinq;
using Newtonsoft.Json;

namespace BootCamp.Chapter.Commands.Output
{
    [Command("time")]
    public class TimeCommand : OutputCommand
    {
        [CommandOption("interval", 'i', Description = "eg. 14:00-22:00")]
        public string TimeInterval { get; set; }

        private protected override string ProcessCommand()
        {
            var transactions = JsonReader.Read(FilePath);

            var transactionsByTime = CheckByTime(transactions).ToList();
            var rushHour = transactionsByTime.MaxBy(n => n.Hour).Select(n => n.Hour);

            return JsonConvert.SerializeObject(new {Summary = transactionsByTime, RushHour = rushHour},
                Formatting.Indented);
        }

        private IEnumerable<SummaryByTime> CheckByTime(IEnumerable<Transaction> transactions)
        {
            var timeInterval = GetTimeInterval();

            var emptyDb = new List<SummaryByTime>();
            for (var i = timeInterval[0]; i < timeInterval[1]; i++) emptyDb.Add(new SummaryByTime(i));

            var transactionDatabase = transactions.GroupBy(n => n.DateTime, (date, values) =>
                {
                    var transaction = values.ToList();
                    return new
                    {
                        Date = date,
                        Price = transaction.Select(n => n.Price).Sum(),
                        Count = transaction.Select(n => n.Price).Count()
                    };
                })
                .GroupBy(n => n.Date.Hour, (hour, values) =>
                {
                    var transaction = values.ToList();
                    return new SummaryByTime(hour, transaction.Select(n => n.Price).Average(),
                        transaction.Select(n => n.Count).Sum());
                })
                .Where(n => n.Hour >= timeInterval[0] && n.Hour <= timeInterval[1])
                .Union(emptyDb)
                .DistinctBy(n => n.Hour)
                .OrderBy(n => n.Hour);

            return transactionDatabase;
        }

        private List<int> GetTimeInterval()
        {
            if (string.IsNullOrEmpty(TimeInterval)) return new List<int> {0, 23};

            var timeInterval = TimeInterval.Split('-');
            if (timeInterval.Length != 2) throw new InvalidCommandException();

            var isBeginValid = DateTimeOffset.TryParse(timeInterval[0], Config.CultureInfo, DateTimeStyles.None,
                out var timeBegin);
            var isEndValid = DateTimeOffset.TryParse(timeInterval[1], Config.CultureInfo, DateTimeStyles.None,
                out var timeEnd);
            if (!isBeginValid || !isEndValid) throw new InvalidCommandException();

            var timeBeginHour = timeBegin.Hour;
            var timeEndHour = timeEnd.Hour == 0 ? 23 : timeEnd.Hour - 1;
            if (timeBeginHour >= timeEndHour) throw new InvalidCommandException();

            return new List<int> {timeBeginHour, timeEndHour};
        }
    }
}
