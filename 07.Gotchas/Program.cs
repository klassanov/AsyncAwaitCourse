using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _07.Gotchas
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //RunAsyncVoid();
            //AsyncVoidLambda();
            await NestedTasks();
        }

        //Exception cannot be caught in async void
        static void RunAsyncVoid()
        {
            try
            {
                AsyncVoid();
            }
            catch (Exception ex)
            {
                Console.WriteLine("The exception cannot be caught!");
            }
        }

        public static async void AsyncVoid()
        {
            await Task.Run(() =>
            {
                Console.WriteLine("Message");
            });

            throw new InvalidOperationException("Some invalid operation error");
        }

        public static async void AsyncVoidLambda()
        {
            try
            {
                var list = new List<int> { 1, 2, 3, 4, 5, 6 };
                list.ForEach(async (x) =>
               {
                   await Task.Run(() => Console.WriteLine(x));
                   throw new InvalidOperationException("In a lambda!");
               });

            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot be caught");
            }
        }

        public static async Task NestedTasks()
        {
            await Task.Run(async () =>
            {
                Console.WriteLine("Before delay");
                await Task.Delay(1000);
                Console.WriteLine("After delay");
            });

            Console.WriteLine("After a task");
        }
    }
}
