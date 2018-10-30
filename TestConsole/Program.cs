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
        public static void TestJson()
        {
            var test = JsonConvert.SerializeObject(new JsonObject {NeedToAddPrefix = 123});

            var result = JsonConvert.DeserializeObject<JsonObject>(test);

            Console.WriteLine(test);
        }

        private static void Main(string[] args)
        {
            TestJson();
            Console.ReadKey();
        }
    }
}
