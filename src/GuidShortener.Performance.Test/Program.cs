using System;
using System.Diagnostics;

namespace GuidShortener.Performance.Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var timer = new Stopwatch();
            Console.WriteLine("Performance Test!");


            var guidArray = new Guid[100000];
            var stringArray = new string[100000];


            Array.Fill(guidArray, Guid.NewGuid());
            timer.Start();
            Array.ForEach(guidArray, x => GuidShortener.ToB32String(x));
            timer.Stop();
            Console.WriteLine($"1,000,000 Guid -> base32string = ({timer.Elapsed.TotalMilliseconds})ms");

            timer.Reset();
            Array.Fill(stringArray, GuidShortener.ToB32String(Guid.NewGuid()));
            timer.Start();
            Array.ForEach(stringArray, x => GuidShortener.FromB32ToGuid(x));
            timer.Stop();
            Console.WriteLine($"1,000,000 base32string -> Guid = ({timer.Elapsed.TotalMilliseconds})ms");

            timer.Reset();
            Array.Fill(guidArray, Guid.NewGuid());
            timer.Start();
            Array.ForEach(guidArray, x => GuidShortener.ToB64String(x));
            timer.Stop();
            Console.WriteLine($"1,000,000 Guid -> base64string = ({timer.Elapsed.TotalMilliseconds})ms");

            timer.Reset();
            Array.Fill(stringArray, GuidShortener.ToB64String(Guid.NewGuid()));
            timer.Start();
            Array.ForEach(stringArray, x => GuidShortener.FromB64ToGuid(x));
            timer.Stop();
            Console.WriteLine($"1,000,000 base64string -> Guid = ({timer.Elapsed.TotalMilliseconds})ms");
        }
    }
}
