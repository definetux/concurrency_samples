using System;
using System.Threading;

namespace Concurrency.Multithreading
{
    public class UsingThreadPool : IExample
    {
        public void Run()
        {
            ThreadPool.QueueUserWorkItem(LongTimeWork);
            ThreadPool.QueueUserWorkItem(LongTimeWorkWithParameter, 500000);
        }

        private void LongTimeWork(object state)
        {
            var result = executeWork(1000000000);
            Console.WriteLine($"Method without parameters returns {result}");
        }

        private void LongTimeWorkWithParameter(object count)
        {
            var result = executeWork((int)count);
            Console.WriteLine($"Method with parameters returns {result}");
        }

        private double executeWork(int count)
        {
            double result = 0;
            for (var i = 0; i < count; i++)
            {
                result += i;
            }
            return result;
        }
    }
}