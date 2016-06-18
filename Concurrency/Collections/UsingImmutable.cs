using System;
using System.Collections.Immutable;

namespace Concurrency.Collections
{
    public class UsingImmutable : IExample
    {
        public void Run()
        {
            ShowImmutableStack();
            ShowImmutableList();
        }

        private void ShowImmutableList()
        {
            var list = ImmutableList<int>.Empty;
            list = list.Add(2);
            list = list.Insert(0, 1);
            list = list.Add(3);

            Console.WriteLine("Before removing: ");
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }

            list.Remove(2);

            Console.WriteLine("After removing: ");
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }

        private void ShowImmutableStack()
        {
            var stack = ImmutableStack<int>.Empty;
            stack = stack.Push(1);
            stack = stack.Push(2);
            stack = stack.Push(3);
            stack = stack.Pop();
            stack = stack.Pop();

            foreach (var item in stack)
            {
                Console.WriteLine(item);
            }
        }
    }
}