using System;
using System.Globalization;
using System.Threading;
namespace RhythmsGonnaGetYou
{
    class Program
    {
        static void Main(string[] args)
        {
            var timeSpan = "00:02:30";
            TimeSpan ts = TimeSpan.Parse(timeSpan);
            Console.WriteLine(ts);

        }
    }
}
