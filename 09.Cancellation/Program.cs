using System;
using System.Threading;
using System.Threading.Tasks;

namespace _09.Cancellation
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var cancellation = new CancellationTokenSource();
            //var cancellation = new CancellationTokenSource(2000); //cancel by time

            var printTask = Task.Run(async () =>
            {
                while (true)
                {
                    if (cancellation.IsCancellationRequested)
                    {
                        Console.WriteLine("Enough");
                        break;
                    }

                    Console.WriteLine(DateTime.Now);
                    await Task.Delay(2000);
                }
            }, cancellation.Token);


            var readTask = Task.Run(() =>
            {
                while (true)
                {
                    var input = Console.ReadLine();
                    if (input == "end")
                    {
                        cancellation.Cancel();
                        break;
                    }
                }
            });

            await Task.WhenAll(printTask, readTask);
        }
    }
}
