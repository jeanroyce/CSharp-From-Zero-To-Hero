using System;
using System.Globalization;
using BootCamp.Chapter.Exceptions;
using Newtonsoft.Json;

namespace BootCamp.Chapter.Models
{
    public class Transaction
    {
        public string Shop { get; }
        public string City { get; }
        public string Street { get; }
        public string Item { get; }
        public DateTimeOffset DateTime { get; }
        public decimal Price { get; }
        
        [JsonConstructor]
        public Transaction(string shop, string city, string street, string item, string dateTime, string price)
        {
            Shop = shop ?? throw new ArgumentNullException(nameof(shop));
            City = city ?? throw new ArgumentNullException(nameof(city));
            Street = street ?? throw new ArgumentNullException(nameof(street));
            Item = item ?? throw new ArgumentNullException(nameof(item));
            
            var isDateTimeValid = DateTimeOffset.TryParse(dateTime, Config.CultureInfo, DateTimeStyles.None, out var dateTimeParsed);
            var isPriceValid = decimal.TryParse(price,NumberStyles.Any, Config.CultureInfo, out var priceParsed);
            if (!isDateTimeValid || !isPriceValid) throw new InvalidTransactionsFileException();

            DateTime = dateTimeParsed;
            Price = priceParsed;
        }
    }
}
