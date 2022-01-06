using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace AsyncAwait
{
    public class Program
    {
        //Rewrite the 04.TaskMethods with async/await

        static async Task Main(string[] args)
        {
            //await WaitForTask();
            //await TaskContinuation();
            //await TaskExceptionsAndStatus();
            //await MultipleTasksAtTheSameTime();
            //await AtLeastOneTaskToFinish();
            //await CompletedTaskAndFromResult();
            //await DownloadFileContentAndSaveItToFile();
            AsyncLambda();
        }

        static async Task WaitForTask()
        {
            //Task firstTask = Task.Run(() => { Console.WriteLine("First task"); });

            //Task<string> secondTask = Task.Run(() => "Second task");

            //Console.WriteLine("Just a sync console writeline");

            //firstTask.Wait();

            //var result = secondTask.Result;

            //Console.WriteLine(result);

            Task firstTask = Task.Run(() => { Console.WriteLine("First task"); });

            Task<string> secondTask = Task.Run(() => "Second task");

            Console.WriteLine("Just a sync console writeline");

            //The difference between Wait and await is that the await does not block
            await firstTask;

            var result = await secondTask;

            Console.WriteLine(result);
        }

        static async Task TaskContinuation()
        {
            //Task task = Task.Run(() => "First Step")
            //    .ContinueWith(prevTask =>
            //    {
            //        Console.WriteLine(prevTask.Result);
            //    })
            //    .ContinueWith(prevTask =>
            //    {
            //        Task.Delay(500).Wait();
            //    })
            //    .ContinueWith(prevTask =>
            //    {
            //        Console.WriteLine("Last Step");
            //    });

            //task.Wait();

            //--------------------------------
            string taskResult = await Task.Run(() => "First Step");
            Console.WriteLine(taskResult);

            await Task.Delay(500);
            Console.WriteLine("Last Step");

        }

        static async Task TaskExceptionsAndStatus()
        {
            //var task = Task
            //    .Run(() => throw new InvalidOperationException("Some exception"))
            //    .ContinueWith(prevTask =>
            //    {
            //        if (prevTask.IsFaulted)
            //        {
            //            Console.WriteLine(prevTask.Exception.Message);
            //        }
            //    })
            //    .ContinueWith(prevTask =>
            //    {
            //        if (prevTask.IsCompletedSuccessfully)
            //        {
            //            Console.WriteLine("Done");
            //        }
            //    });

            //task.Wait();


            //The try/catch block works with async await
            try
            {
                await Task.Run(() => throw new InvalidOperationException("Some exception"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Done");
        }

        static async Task MultipleTasksAtTheSameTime()
        {
            //var firstTask = Task.Run(() => Task.Delay(3000).Wait())
            //                .ContinueWith(_ => Console.WriteLine("First"));

            //var secondTask = Task.Run(() => Task.Delay(1000).Wait())
            //                .ContinueWith(_ => Console.WriteLine("Second"));

            //var thirdTask = Task.Run(() => Task.Delay(2000).Wait())
            //                .ContinueWith(_ => Console.WriteLine("Third"));

            //This also blocks and waits until all of them are executed, but they are executed at the same time, 
            //so the time we wait is equal to the execution time of the longest task
            //Task.WaitAll(firstTask, secondTask, thirdTask);


            var firstTask = Task.Run(async () =>
            {
                await Task.Delay(3000);
                Console.WriteLine("First");
            });

            var secondTask = Task.Run(async () =>
            {
                await Task.Delay(1000);
                Console.WriteLine("Second");
            });

            var thirdTask = Task.Run(async () =>
            {
                await Task.Delay(2000);
                Console.WriteLine("Third");
            });

            //Use WhenAll (instead of WaitAll, which is non-blocking and awaitable). This is an important difference
            await Task.WhenAll(firstTask, secondTask, thirdTask);
        }

        static async Task AtLeastOneTaskToFinish()
        {
            //Console.WriteLine("You have 10 seconds to solve 10*10");


            //var inputTask = Task.Run(() =>
            //{
            //    while (true)
            //    {
            //        var answer = Console.ReadLine();
            //        if (answer.Equals("100"))
            //        {
            //            Console.WriteLine("Correct!");
            //            break;
            //        }
            //        Console.WriteLine("Wrong answer");
            //    }
            //});

            //var timerTask = Task.Run(() =>
            //{
            //    for (int i = 10; i > 0; i--)
            //    {
            //        Console.WriteLine(i);

            //        Task.Delay(1000).Wait();
            //    }

            //    Console.WriteLine("Timeout");
            //});

            ////Operation Timer implementation
            //Task.WaitAny(timerTask, inputTask);

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

            var timerTask = Task.Run(async () =>
            {
                for (int i = 10; i > 0; i--)
                {
                    Console.WriteLine(i);

                    await Task.Delay(1000);
                }

                Console.WriteLine("Timeout");
            });

            //Operation Timer implementation
            await Task.WhenAny(timerTask, inputTask);
        }

        static async Task CompletedTaskAndFromResult()
        {
            //Console.WriteLine("Please enter your input");

            //while (true)
            //{
            //    var input = Console.ReadLine();

            //    if (input == "end")
            //    {
            //        break;
            //    }

            //    //New switch syntax
            //    var task = input switch
            //    {
            //        "delay" => Task
            //                .Delay(3000)
            //                .ContinueWith(_ => Console.WriteLine("Delayed")),

            //        "print" => Task
            //                .Run(() => Console.WriteLine("Printed")),

            //        "throw" => Task
            //                .FromException(new InvalidOperationException("Some error on invalid operation"))
            //                .ContinueWith(prev => Console.WriteLine(prev.Exception.Message)),

            //        "76" => Task
            //                .FromResult(76)
            //                .ContinueWith(prev => Console.WriteLine(prev.Result)),

            //        //default case
            //        _ => Task
            //             .CompletedTask
            //             .ContinueWith(_ => Console.WriteLine("Invalid input"))
            //    };

            //    task.Wait();
            //}



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

                await task;
            }

        }

        static async Task DownloadFileContentAndSaveItToFile()
        {
            //using (var client = new HttpClient())
            //{
            //    Task<string> googleTask = client.GetStringAsync("https://google.com");
            //    googleTask.ContinueWith(prev =>
            //    {
            //        File.WriteAllTextAsync("google.txt", prev.Result)
            //            .Wait();
            //    });


            //    var prevodiTask = client.GetStringAsync("http://prevodilegalizacia.bg");
            //    prevodiTask.ContinueWith(prev =>
            //    {
            //        File.WriteAllTextAsync("prevodi.txt", prev.Result)
            //            .Wait();
            //    });

            //    var tasks = new Task<string>[] { googleTask, prevodiTask };

            //    Task.WhenAll(tasks)
            //        .ContinueWith(prev =>
            //        {
            //            var content = $"{prev.Result[0]}{prev.Result[1]}";
            //            File.WriteAllTextAsync("merge.txt", content)
            //                .Wait();
            //        })
            //        .Wait();
            //}


            using (var client = new HttpClient())
            {
                Task<string> googleTask = client.GetStringAsync("https://google.com");
                Task<string> prevodiTask = client.GetStringAsync("http://prevodilegalizacia.bg");
                var getTasks = new Task<string>[]
                {
                    googleTask, prevodiTask
                };

                //More elegant way to write it
                //var getTasks2 = new List<Task<string>>
                //{
                //    client.GetStringAsync("https://google.com"),
                //    client.GetStringAsync("http://prevodilegalizacia.bg")
                //};


                string[] results = await Task.WhenAll(getTasks);

                //More elegant way to write it, but you neeed to write a deconstructor, please see the Extensions file
                //var (google, prevodi) = await Task.WhenAll(getTasks2);


                Task writeGoogleFileTask = File.WriteAllTextAsync("google.txt", results[0]);
                Task writePrevodiTask = File.WriteAllTextAsync("prevodi.txt", results[1]);

                //More elegant way to write it
                //var writeTasks = new Task[]
                //{
                //    writeGoogleFileTask, writePrevodiTask
                //};

                // WhenAll has also params overload
                await Task.WhenAll(writeGoogleFileTask, writePrevodiTask);

                var content = $"{results[0]}{results[1]}";
                await File.WriteAllTextAsync("merge.txt", content);

            }
        }

        static void AsyncLambda()
        {
            //Non async method but using async await inside
            //But attention! This is async void method, not to use
            var list = new List<int> { 1, 2, 3, 4, 5 };
            list.ForEach(async x =>
            {
                await Task.Run(() => Console.WriteLine(x));
            });
        }
    }
}
