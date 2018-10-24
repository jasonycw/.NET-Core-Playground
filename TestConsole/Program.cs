using Itenso.TimePeriod;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestConsole
{
    internal class Program
    {
        public static void TestDateTime()
        {
            var startTime = new DateTime(2018, 10, 24, 10, 30, 00);
            var endTime = new DateTime(2018, 10, 24, 10, 00, 00);
            var duration = startTime - endTime;
            Console.WriteLine(duration);
        }

        private static void Main(string[] args)
        {
            TestDateTime();
            Console.ReadKey();
        }
    }
}
