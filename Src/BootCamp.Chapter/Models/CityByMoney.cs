namespace BootCamp.Chapter.Models
{
    public readonly struct CityByMoney
    {
        public string City { get; }
        public decimal Money { get; }
        
        public CityByMoney(string city, decimal money)
        {
            City = city;
            Money = money;
        }
    }
}
