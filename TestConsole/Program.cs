using Itenso.TimePeriod;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using TestConsole.TestJson;

namespace TestConsole
{
    internal class Program
    {
        public static void TestCalculation()
        {
            var totalCount = 5;
            var size = 2;
            int count = (totalCount - 1) / size + 1;
            Console.WriteLine(count);
        }

        private static void Main(string[] args)
        {
            TestCalculation();
            Console.ReadKey();
        }
    }
}
