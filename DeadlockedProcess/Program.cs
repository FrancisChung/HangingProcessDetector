using System;
using System.Threading;

class Program
{
    static object lock1 = new object();
    static object lock2 = new object();

    static void Main(string[] args)
    {
        // Create two threads and start them
        Thread thread1 = new Thread(Thread1Method);
        Thread thread2 = new Thread(Thread2Method);

        thread1.Start();
        thread2.Start();

        // Wait for both threads to finish
        thread1.Join();
        thread2.Join();

        Console.WriteLine("Both threads finished.");
    }

    static void Thread1Method()
    {
        lock (lock1)
        {
            Console.WriteLine("Thread 1 acquired lock 1.");

            // Introduce a delay to make it easier to observe the deadlock
            Thread.Sleep(100);

            Console.WriteLine("Thread 1 waiting to acquire lock 2.");

            // Now, try to acquire lock2
            lock (lock2)
            {
                Console.WriteLine("Thread 1 acquired lock 2.");
            }
        }
    }

    static void Thread2Method()
    {
        lock (lock2)
        {
            Console.WriteLine("Thread 2 acquired lock 2.");

            // Introduce a delay to make it easier to observe the deadlock
            Thread.Sleep(100);

            Console.WriteLine("Thread 2 waiting to acquire lock 1.");

            // Now, try to acquire lock1
            lock (lock1)
            {
                Console.WriteLine("Thread 2 acquired lock 1.");
            }
        }
    }
}