using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BurgerPreparation
{
    class Program
    {
        /*  Notes:
             - Tasks not necessarily start threads 
             - Multithreaded: many "worksers" vs Async: one "worker" this is the difference
             - Each "worker" (= thread) can work asynchroniously
             - Some tasks create new threads (for ex. Task.Run(...)), but this decision is taken automatically by the framework
         */

        static async Task Main(string[] args)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            //Way 1 - Everything is synchronous - the slowest version
            //SyncCooker.Work();

            //Way 2 - Everything is asynchronous - Problem: dependencies not respected
            //Ex. cannot eat burgers before they are ready
            //await AllAtOnceCooker.Work();

            //Way 3 - Mixed: Execute asynchronously groups of tasks so that we can respect the dependencies
            //The most correct way
            await AsyncCooker.Work();

            stopwatch.Stop();
            Console.WriteLine($"Elaplsed time [ms]: {stopwatch.Elapsed.Milliseconds}");
        }
    }
}
