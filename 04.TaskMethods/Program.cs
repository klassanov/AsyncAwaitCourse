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
            //TaskExceptionsAndStatus();
            //MultipleTasksAtTheSameTime();
            //AtLeastOneTaskToFinish();
            CompletedTaskAndFromResult();
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

        static void MultipleTasksAtTheSameTime()
        {
            var firstTask = Task.Run(() => Task.Delay(3000).Wait())
                            .ContinueWith(_ => Console.WriteLine("First"));

            var secondTask = Task.Run(() => Task.Delay(1000).Wait())
                            .ContinueWith(_ => Console.WriteLine("Second"));

            var thirdTask = Task.Run(() => Task.Delay(2000).Wait())
                            .ContinueWith(_ => Console.WriteLine("Third"));

            //This also blocks and waits until all of them are executed, but they are executed at the same time, 
            //so the time we wait is equal to the execution time of the longest task
            Task.WaitAll(firstTask, secondTask, thirdTask);

        }

        static void AtLeastOneTaskToFinish()
        {
            //Operation timeout implementation: the one that finishes first

            //Note how even though the console output might be broken, the input and output are not confused
            Console.WriteLine("You have 10 seconds to solve 10*10");


            var inputTask = Task.Run(() =>
            {
                while (true)
                {
                    var answer = Console.ReadLine();
                    if (answer.Equals("100"))
                    {
                        Console.WriteLine("Correct!");
                        break;
                    }
                    Console.WriteLine("Wrong answer");
                }
            });

            var timerTask = Task.Run(() =>
            {
                for (int i = 10; i > 0; i--)
                {
                    Console.WriteLine(i);

                    Task.Delay(1000).Wait();
                }

                Console.WriteLine("Timeout");
            });

            //Operation Timer implementation
            Task.WaitAny(timerTask, inputTask);
        }

        static void CompletedTaskAndFromResult()
        {
            //Example of CompletedTask, FromResult, FromException => useful when we have the return result ready and
            //there is no sense in launching a new complex long-running task
            Console.WriteLine("Please enter your input");

            while (true)
            {
                var input = Console.ReadLine();

                if (input == "end")
                {
                    break;
                }

                //New switch syntax
                var task = input switch
                {
                    "delay" => Task
                            .Delay(3000)
                            .ContinueWith(_ => Console.WriteLine("Delayed")),

                    "print" => Task
                            .Run(() => Console.WriteLine("Printed")),

                    "throw" => Task
                            .FromException(new InvalidOperationException("Some error on invalid operation"))
                            .ContinueWith(prev => Console.WriteLine(prev.Exception.Message)),

                    "76" => Task
                            .FromResult(76)
                            .ContinueWith(prev => Console.WriteLine(prev.Result)),

                    //default case
                    _ => Task
                         .CompletedTask
                         .ContinueWith(_ => Console.WriteLine("Invalid input"))
                };

                task.Wait();
            }
        }
    }
}
