using System;
using System.Threading;

namespace TrackThread
{
    public class MyThreadClass
    {
        // Loops 3 times, suspending for 0.5 seconds.
        public static void Thread1()
        {
            for (int loopCount = 0; loopCount <= 2; loopCount++)
            {
                Thread thread = Thread.CurrentThread;
                Console.WriteLine("Name of Thread: " + thread.Name + " Process = " + loopCount);
                Thread.Sleep(500);
            }
        }

        // Loops six times, suspending for 1.5 seconds.
        public static void Thread2()
        {
            for (int loopCount = 0; loopCount < 6; loopCount++)
            {
                Thread thread = Thread.CurrentThread;
                Console.WriteLine("Name of Thread: " + thread.Name + " Process = " + loopCount);
                Thread.Sleep(1500);
            }
        }
    }
}
