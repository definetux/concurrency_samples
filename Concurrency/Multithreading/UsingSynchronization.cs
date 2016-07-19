using System;
using System.Threading;

namespace Concurrency.Multithreading
{
    public class UsingSynchronization : IExample
    {
        private string _sharedState = "None";
        private readonly object _lock = new object();

        private readonly Mutex _mutex = new Mutex();
        private readonly AutoResetEvent _mre = new AutoResetEvent(true); 

        public void Run()
        {
            new Thread(RunOwnWork).Start("Thread 1");
            new Thread(RunOwnWork).Start("Thread 2");
        }

        private void RunOwnWork(object threadName)
        {
            while (true)
            {
                DoWork(threadName);
            }
        }

        private void DoWork(object threadName)
        {
            Console.WriteLine($"{threadName} is entered");

            lock (_lock) // _mre.WaitOne(); // _mutex.WaitOne();
            {
                Console.WriteLine($"{threadName} does long work");

                _sharedState = threadName.ToString();
                Thread.Sleep(2000);
                Console.WriteLine($"Shared state is {_sharedState}\n");

                if (!_sharedState.Equals(threadName.ToString()))
                {
                    throw new InvalidOperationException("Synchronizarion failed");
                }
            }
            //_mre.Set(); //_mutex.ReleaseMutex();

            Console.WriteLine($"{threadName} is left");
        }
    }
}