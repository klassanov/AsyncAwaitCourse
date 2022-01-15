using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace CustomAwait
{
    public static class Awaiters
    {
        //Everything that has a method named GetAwaiter can be awaited.
        //So, we define these methods and then we await
        public static TaskAwaiter<string> GetAwaiter(this Uri uri)
        {
            return new HttpClient().GetStringAsync(uri).GetAwaiter();
        }

        //Cool syntax
        public static TaskAwaiter GetAwaiter(this TimeSpan timeSpan)
            => Task.Delay(timeSpan).GetAwaiter();


        public static TaskAwaiter GetAwaiter(this IEnumerable<Task> tasks)
            => Task.WhenAll(tasks).GetAwaiter();

    }
}
