using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Concurrency.Collections;
using Concurrency.Multithreading;
using Concurrency.Parallelism;

namespace Concurrency
{
    class Program
    {
        static void Main(string[] args)
        {
            IExample example = new UsingThread();
            example.Run();
            Console.WriteLine("Example runned");
        
            Console.ReadLine();
        }
    }
}
