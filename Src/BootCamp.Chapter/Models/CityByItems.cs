﻿namespace BootCamp.Chapter.Models
{
    public readonly struct CityByItems
    {
        public string City { get; }
        public int Count { get; }

        public CityByItems(string city, int count)
        {
            City = city;
            Count = count;
        }
    }
}
