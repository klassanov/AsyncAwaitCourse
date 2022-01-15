using System;

namespace _10.TasksBehindTheScenes
{
    class EventBasedApi
    {
        //Defines an event
        public event Action<string> OnDone;

        public void Work()
        {
            Console.WriteLine("Working...");

            var end = DateTime.Now.AddSeconds(5);

            while (true)
            {
                if (DateTime.Now > end)
                {
                    break;
                }
            }

            Console.WriteLine("Firing OnDone");


            //Raises the event
            this.OnDone("Data");
        }
    }
}
