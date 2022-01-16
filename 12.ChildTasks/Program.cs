using System;
using System.Threading.Tasks;

namespace _12.ChildTasks
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //Detached tasks are recomended
            //This is the best implementation - everything is awaited
            await DetachedAsync();

            //Same code with 1 difference, the task creation option
            //Detached();

            //Attached();

            //await DetachedMyTest();
            //await AttachedMyTest();
        }


        static async Task DetachedAsync()
        {
            await Task.Run(async () =>
            {
                Console.WriteLine("First task detached async");

                await Task.Run(() =>
                {
                    CustomDelay(5000);
                    Console.WriteLine("Second task detached async");
                });
            });
        }

        static void Detached()
        {
            Task.Factory
                .StartNew(() =>
                {
                    Console.WriteLine("First task detached");
                    Task.Factory
                        .StartNew(() =>
                        {
                            //This is not awaited since it is detached from the parent task
                            Task.Delay(5000).Wait();
                            Console.WriteLine("Second task detached");
                        });
                })
                .Wait();
        }

        static void Attached()
        {
            Task.Factory
                .StartNew(() =>
                {
                    Console.WriteLine("First task attached");

                    Task.Factory
                        .StartNew(() =>
                        {
                            //This is awaited by the parent since it is attached to the parent task
                            Task.Delay(5000).Wait();
                            Console.WriteLine("Second task attached");
                        },
                        TaskCreationOptions.AttachedToParent);
                })
                .Wait();
        }

        static async Task DetachedMyTest()
        {
            await
            Task.Factory
                .StartNew(() =>
                {
                    Console.WriteLine("First task detached my test");
                    Task.Factory
                        .StartNew(() =>
                        {
                            //This is not awaited since it is detached from the parent task
                            CustomDelay(5000);
                            Console.WriteLine("Second task detached my test");
                        });
                });
        }

        static async Task AttachedMyTest()
        {
            await
            Task.Factory
                .StartNew(() =>
                {
                    Console.WriteLine("First task attached my test");
                    Task.Factory
                        .StartNew(() =>
                        {
                            //This is awaited
                            CustomDelay(5000);
                            Console.WriteLine("Second task attached my test");
                        }, TaskCreationOptions.AttachedToParent);
                });
        }

        private static void CustomDelay(int milliseconds)
        {
            var end = DateTime.Now.AddMilliseconds(milliseconds);

            while (true)
            {
                if (DateTime.Now > end)
                {
                    break;
                }
            }
        }
    }
}
