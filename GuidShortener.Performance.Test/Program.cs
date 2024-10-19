using System;
using System.Diagnostics;
using System.Linq;

namespace GuidShortener.Performance.Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var timer = new Stopwatch();
            Console.WriteLine("Performance Test!");

            var size = 1_000_000;

            var guidArray = new Guid[size];
            var stringArray = new string[size];

            // test 1
            guidArray = Enumerable.Range(0, size)
                             .Select(_ => Guid.NewGuid())
                             .ToArray();
            timer.Start();
            Array.ForEach(guidArray, x => GuidShortener.ToB32String(x));
            timer.Stop();
            Console.WriteLine($"{size} Guid -> base32string = ({timer.Elapsed.TotalMilliseconds})ms");


            timer.Reset();
            // test 2
            stringArray = Enumerable.Range(0, size)
                 .Select(_ => GuidShortener.ToB32String(Guid.NewGuid()))
                 .ToArray();
            timer.Start();
            Array.ForEach(stringArray, x => GuidShortener.FromB32ToGuid(x));
            timer.Stop();
            Console.WriteLine($"{size} base32string -> Guid = ({timer.Elapsed.TotalMilliseconds})ms");


            timer.Reset();
            // test 3
            guidArray = Enumerable.Range(0, size)
                             .Select(_ => Guid.NewGuid())
                             .ToArray();
            timer.Start();
            Array.ForEach(guidArray, x => GuidShortener.ToB64String(x));
            timer.Stop();
            Console.WriteLine($"{size} Guid -> base64string = ({timer.Elapsed.TotalMilliseconds})ms");


            timer.Reset();
            // test 4
            stringArray = Enumerable.Range(0, size)
                 .Select(_ => GuidShortener.ToB64String(Guid.NewGuid()))
                 .ToArray();
            timer.Start();
            Array.ForEach(stringArray, x => GuidShortener.FromB64ToGuid(x));
            timer.Stop();
            Console.WriteLine($"{size} base64string -> Guid = ({timer.Elapsed.TotalMilliseconds})ms");
        }
    }
}
