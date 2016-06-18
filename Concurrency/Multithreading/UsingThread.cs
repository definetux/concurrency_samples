using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Concurrency.Multithreading
{
    public class UsingThread : IExample
    {
        public void Run()
        {
            var thread = new Thread(LongTimeWork);
            var threadWithParameters = new Thread(LongTimeWorkWithParameter);
            thread.Start();
            threadWithParameters.Start(5000000);
        }

        private void LongTimeWork()
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