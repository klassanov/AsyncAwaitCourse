using System;
using System.Threading.Tasks;

namespace FromSyncToAync
{
    class Program
    {
        static void Main(string[] args)
        {
            SimpleExample();
        }

        static void SimpleExample()
        {
            //We cannot be sure that the output will be "First" and then "Second"
            //It can be both ways, there is no warranty of the order of execution

            var task = Task.Run(() => { Console.WriteLine("1.First"); });

            Console.WriteLine("2.Second");

            //Wait is not the proper way to use tasks
            task.Wait();
        }
    }
}
