﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Itenso.TimePeriod;
using Newtonsoft.Json;
using TestConsole.Common;
using TestConsole.Domain.Core;
using TestConsole.Domain.Test;
using TestConsole.Domain.Test.Events;
using TestConsole.Domain.Test.Handler;
using TestConsole.TestJson;
using TestConsole.TestNamespace.Some.AnotherFunction;

namespace TestConsole
{
    internal class OldTest
    {
        public static void TestJson()
        {
            var test = JsonConvert.SerializeObject(new JsonObject {NeedToAddPrefix = 123});
            Console.WriteLine(test);
            var result = JsonConvert.DeserializeObject<JsonObject>(test);
            Console.WriteLine(result.NeedToAddPrefix);
        }

        public static void TestTimeRange2()
        {
            var timeRange = new TimeRange(new DateTime(2018, 11, 22, 10, 15, 00), Duration.Minutes(30));

            timeRange.ExpandStartTo(new DateTime(2018, 11, 22, 10, 14, 00));

            Console.WriteLine($"Result: {timeRange}");
        }

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

        public static void TestNamespace()
        {
            var test = new ChildClass();

            var name = test.GetFunctionName();

            Console.WriteLine($"Function: {name}");
        }

        public static void TestJsonConvert()
        {
            var email = Email.Create("jasonycw1992@gmail.com");
            Console.WriteLine($"Email: {email}");

            var json = JsonConvert.SerializeObject(email);
            var jsonEmail = JsonConvert.DeserializeObject(json);
            Console.WriteLine($"Email: {jsonEmail}");
        }

        public static void FakeMediatorPublish(DomainEvent domainEvent)
        {
            switch (domainEvent)
            {
                case TestCreated e:
                    new TestCreatedHandler().Handle(e, CancellationToken.None);
                    break;
                case TestRenamed e:
                    new TestRenamedHandler().Handle(e, CancellationToken.None);
                    break;
                case TestEmailChanged e:
                    new TestEmailChangedHandler().Handle(e, CancellationToken.None);
                    break;
                default:
                    Console.WriteLine($"Type: {domainEvent.Type}\t EntityType: {domainEvent.EntityType}\n EntityId: {domainEvent.EntityId}\t Data: {JsonConvert.SerializeObject(domainEvent.Data)}");
                    break;
            }
        }

        public static void TestDomainEventType()
        {
            var test1 = Test.Create("test1", Email.Create("asf@qwr.com"));
            test1.Rename("rename1");
            var test2 = Test.Create("test2", Email.Create("qwer@asdf.com"));
            test1.Rename("rename2");
            test2.ChangeEmail(Email.Create("123@123.123"));
            test2.Rename("test2 renamed");
            test1.Rename("rename3");

            Console.WriteLine($"---- test1 event count: {test1.Events.Count}");
            foreach (var domainEvent in test1.Events)
            {
                FakeMediatorPublish(domainEvent);
            }

            Console.WriteLine();
            Console.WriteLine($"---- Event2 count: {test2.Events.Count}");
            foreach (var domainEvent in test2.Events)
            {
                FakeMediatorPublish(domainEvent);
            }
        }

        public static void TestJsonSerialize()
        {
            var email = new EmailRequest()
            {
                Attachment = new Attachment()
                {
                    ContentType = "content type",
                    File = "a file",
                    FileName = "a name"
                },
                Body = "a body",
                From = "f",
                To = "t",
                Subject = "A Subject"
            };

            Console.WriteLine(email.ToJson());
        }

        public static void TestTimeRange()
        {
            var selected = new TimeBlock(new DateTime(2018, 10, 10, 09, 00, 0), TimeSpan.FromMinutes(30));
            var candidate = new List<TimeRange>
            {
                new TimeRange(new DateTime(2018, 10, 10, 08, 00, 0), TimeSpan.FromMinutes(30)),
                //new TimeRange(new DateTime(2018, 10, 10, 08, 30, 0), TimeSpan.FromMinutes(30)),
                new TimeRange(new DateTime(2018, 10, 10, 09, 30, 0), TimeSpan.FromMinutes(30)),
                new TimeRange(new DateTime(2018, 10, 10, 10, 00, 0), TimeSpan.FromMinutes(30)),
            };
            var result = candidate.OrderBy(t => t.Start.Subtract(selected.Start).Duration()).FirstOrDefault();

            Console.WriteLine($"Selected: {selected}");
            Console.WriteLine($"Candidate: {String.Join(",", candidate)}");

            foreach (var t in candidate)
            {
                Console.WriteLine($"Diff: {t.Start.Subtract(selected.Start).Duration()}");
            }

            Console.WriteLine($"Result: {result}");
        }

        public static void TestDateTime()
        {
            var startTime = new DateTime(2018, 10, 24, 10, 30, 00);
            var endTime = new DateTime(2018, 10, 24, 10, 00, 00);
            var duration = startTime - endTime;
            Console.WriteLine(duration);
        }
    }
}
