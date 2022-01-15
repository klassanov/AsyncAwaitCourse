using System;
using System.Threading;
using System.Threading.Tasks;

namespace _10.TasksBehindTheScenes
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //var result = await RunAsync(() => "RunAsync task");
            //Console.WriteLine(result);

            //await DelayAsync(2000, () => Console.WriteLine("Run delayed task"));


            //How to wrap in async/await event based apis

            //Classical usage
            //EventBasedApiClassical();

            //Async await wrapper usage
            var result = await EventBasedApiWrapperAsync();
            Console.WriteLine(result);

        }

        static void EventBasedApiClassical()
        {
            EventBasedApi api = new EventBasedApi();
            api.OnDone += args =>
            {
                Console.WriteLine(args);
            };
            api.Work();
        }


        static Task<string> EventBasedApiWrapperAsync()
        {
            var tcs = new TaskCompletionSource<string>();
            var obj = new EventBasedApi();
            obj.OnDone += args =>
            {
                //SetResult or SetException completes the task
                //This will notify the caller of the EventBasedApiWrapper that the task just completed.
                tcs.SetResult(args);
            };

            //You have the full control
            try
            {
                obj.Work();
            }
            catch (Exception ex)
            {
                tcs.SetException(ex);
                throw;
            }

            return tcs.Task;
        }


        public static Task<T> RunAsync<T>(Func<T> function)
        {
            if (function == null)
            {
                throw new InvalidOperationException("function is null");
            }

            var completionSource = new TaskCompletionSource<T>();

            ThreadPool.QueueUserWorkItem(_ =>
            {
                try
                {
                    T result = function();
                    completionSource.SetResult(result);
                }

                catch (Exception ex)
                {
                    completionSource.SetException(ex);
                }
            });

            return completionSource.Task;
        }

        static Task DelayAsync(int milliseconds, Action action)
        {
            if (action == null)
            {
                throw new InvalidOperationException("action is null");
            }

            var tcs = new TaskCompletionSource<object>();

            var timer = new Timer(
                _ => tcs.SetResult(null),
                null,
                milliseconds,
                Timeout.Infinite);

            return tcs.Task.ContinueWith(_ =>
            {
                timer.Dispose();
                action();
            });
        }
    }
}
