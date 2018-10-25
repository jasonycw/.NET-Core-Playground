using System;
using System.Collections.Generic;
using System.Text;
using Itenso.TimePeriod;

namespace TestConsole
{
    public static class ExtensionMethods
    {
        public static ITimePeriodCollection Split(this ITimePeriod timePeriod, TimeSpan duration)
        {
            if(timePeriod.Duration <= duration)
                return new TimePeriodCollection(new List<ITimePeriod>{ timePeriod });
            var result = new List<ITimePeriod> { new TimeRange(timePeriod.Start, duration) };
            result.AddRange(new TimeRange(timePeriod.Start.Add(duration), timePeriod.End).Split(duration));
            return new TimePeriodCollection(result);
        }
    }
}
