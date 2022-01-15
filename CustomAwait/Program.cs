using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomAwait
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var content = await new Uri("http://abv.bg");
            Console.WriteLine(content);


            await TimeSpan.FromMilliseconds(1000);


            await new List<Task>()
            {
                Task.Delay(1000),
                Task.Delay(2000)
            };
        }
    }
}
