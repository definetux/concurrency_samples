using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Concurrency.Collections
{
    public class UsingBlockingCollection : IExample
    {
        private readonly BlockingCollection<int> _bc = new BlockingCollection<int>(); 

        public void Run()
        {
            Parallel.Invoke(WriteItems, ReadItems);
        }

        private void WriteItems()
        {
            var collection = Enumerable.Range(0, 10);
            foreach (var item in collection)
            {
                _bc.Add(item);
                Thread.Sleep(500);
            }
            _bc.CompleteAdding();
        }

        private void ReadItems()
        {
            foreach (var item in _bc.GetConsumingEnumerable())
            {
                Console.WriteLine(item);
            }
        }
    }
}