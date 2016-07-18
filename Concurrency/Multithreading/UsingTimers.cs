using System;
using System.Threading;

namespace Concurrency.Multithreading
{
    public class UsingTimers : IExample
    {
        public void Run()
        {
            var timer = new System.Timers.Timer(TimeSpan.FromSeconds(3).TotalMilliseconds);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();

            var otherTimer = new System.Threading.Timer(TimersCallback, null, 0, 1000);
            // описать disposing таймеров
            // продлевание таймеров изнутри (наложение тиков)
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Console.WriteLine("Event fired");
        }

        private void TimersCallback(object state)
        {
            Console.WriteLine("Callback fired");
        }
    }
}