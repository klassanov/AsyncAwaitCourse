using System;
using System.Threading.Tasks;

namespace _04.TaskMethods
{
    class Program
    {
        static void Main(string[] args)
        {
            //WaitForTask();
            //TaskContinuation();
            TaskExceptionsAndStatus();
        }

        static void WaitForTask()
        {
            Task firstTask = Task.Run(() => { Console.WriteLine("First task"); });

            Task<string> secondTask = Task.Run(() => "Second task");

            Console.WriteLine("Just a sync console writeline");


            //The next 2 instructions make the code synchronous and should not be used

            //Not the right way to do it. Available when the task is void
            firstTask.Wait();

            //Not the right way to do it
            var result = secondTask.Result;

            Console.WriteLine(result);
        }

        static void TaskContinuation()
        {
            Task task = Task.Run(() => "First Step")
                .ContinueWith(prevTask =>
                {
                    Console.WriteLine(prevTask.Result);
                })
                .ContinueWith(prevTask =>
                {
                    Task.Delay(500).Wait();
                })
                .ContinueWith(prevTask =>
                {
                    Console.WriteLine("Last Step");
                });

            task.Wait();
        }

        static void TaskExceptionsAndStatus()
        {
            //Exceptions in tasks should be managed separately: the code will continue anyway
            var task = Task
                .Run(() => throw new InvalidOperationException("Some exception"))
                .ContinueWith(prevTask =>
                {
                    if (prevTask.IsFaulted)
                    {
                        Console.WriteLine(prevTask.Exception.Message);
                    }
                })
                .ContinueWith(prevTask =>
                {
                    if (prevTask.IsCompletedSuccessfully)
                    {
                        Console.WriteLine("Done");
                    }
                });

            task.Wait();
        }
    }
}
