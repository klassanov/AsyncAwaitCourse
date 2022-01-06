using System;

namespace AsyncAwait
{
    public static class Extensions
    {
        public static void Deconstruct<T>(this T[] items, out T first, out T second)
        {
            if (items.Length < 2)
            {
                throw new InvalidOperationException("Items must be 2 to deconstruct them with this method.");
            }

            first = items[0];
            second = items[1];
        }
    }
}
