using Itenso.TimePeriod;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestConsole
{
    internal class Program
    {
        public static void TestTimeGap()
        {
            var schedule = new TimeBlock(new DateTime(2018, 10, 10, 08, 30, 00), Duration.Minutes(180));
            var bookings = new TimePeriodCollection
            {
                new TimeRange(new DateTime(2018, 10, 10, 08, 00, 0), Duration.Minutes(60)),
                new TimeRange(new DateTime(2018, 10, 10, 09, 30, 0), Duration.Minutes(30)),
                new TimeRange(new DateTime(2018, 10, 10, 10, 00, 0), Duration.Minutes(30)),
            };
            var selected = new TimeBlock(new DateTime(2018, 10, 10, 09, 30, 0), Duration.Minutes(30));
            Console.WriteLine($"Selected: {selected}");
            Console.WriteLine();

            // calculate the gaps using the time calendar as period mapper
            var availableTimes = new TimeGapCalculator<TimeBlock>()
                .GetGaps(bookings, schedule)
                .Where(t => t.Duration >= selected.Duration)
                .SelectMany(t => t.Split(selected.Duration));
            foreach (var t in availableTimes)
                Console.WriteLine($"Result: {t}");
        }

        public static void TestTimeRange()
        {
            var timeRange = new TimeRange(new DateTime(2018, 11, 22, 10, 15, 00), Duration.Minutes(30));

            timeRange.ExpandStartTo(new DateTime(2018, 11, 22, 10, 14, 00));

            Console.WriteLine($"Result: {timeRange}");
        }

        private static void Main(string[] args)
        {
            TestTimeRange();
            Console.ReadKey();
        }
    }
}
