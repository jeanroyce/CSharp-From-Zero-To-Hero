namespace BootCamp.Chapter.Models
{
    public readonly struct SummaryByTime
    {
        public int Hour { get; }
        public decimal Earn { get; }
        public int Count { get; }
        
        public SummaryByTime(int hour, decimal earn, int count)
        {
            Hour = hour;
            Earn = earn;
            Count = count;
        }
        
        public SummaryByTime(int hour)
        {
            Hour = hour;
            Earn = 0;
            Count = 0;
        }
    }
}
