using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace Concurrency.Collections
{
    public class UsingThreadSafe : IExample
    {
        //private readonly ConcurrentDictionary<int, string> dictionary = new ConcurrentDictionary<int, string>();
        private readonly Dictionary<int, string> dictionary = new Dictionary<int, string>();

        public void Run()
        {
            new Thread(DoWork).Start(1);
            new Thread(DoWork).Start(2);
            new Thread(DoWork).Start(3);

        }

        private void DoWork(object threadNumber)
        {
            var random = new Random((int) threadNumber);
            while (true)
            {

                AddValue(random.Next(10), $"Thread {threadNumber} is here");

                Console.WriteLine($"Dictionary from Thread {threadNumber}");
                foreach (var item in dictionary)
                {
                    Console.WriteLine($"Key: {item.Key}, Value: {item.Value}");
                }
                Thread.Sleep(1000);
            }
        }

        private void AddValue(int k, string value)
        {
            //dictionary.AddOrUpdate(k, key => value, (key, oldvalue) => value);
            dictionary[k] = value;
        }
    }
}