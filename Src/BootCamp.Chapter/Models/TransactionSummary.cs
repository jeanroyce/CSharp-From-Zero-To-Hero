using System;

namespace BootCamp.Chapter.Models
{
    public class TransactionSummary
    {
        public string City { get; }
        public string Street { get; }
        public string Item { get; }
        public DateTimeOffset DateTime { get; }
        public decimal Price { get; }
        
        public TransactionSummary(string city, string street, string item, DateTimeOffset dateTime, decimal price)
        {
            City = city ?? throw new ArgumentNullException(nameof(city));
            Street = street ?? throw new ArgumentNullException(nameof(street));
            Item = item ?? throw new ArgumentNullException(nameof(item));
            DateTime = dateTime;
            Price = price;
        }
    }
}
