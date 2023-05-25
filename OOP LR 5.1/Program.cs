using System;
using System.Collections.Generic;
using System.Threading;

namespace OOP_LR_5._1
{
    class Program
    {
        private static Queue<int> queue = new Queue<int>();
        private static object lockObject = new object();
        static void Main(string[] args)
        {
            Thread producerThread = new Thread(Producer);
            Thread consumerThread = new Thread(Consumer);

            producerThread.Start();
            consumerThread.Start();

            producerThread.Join();
            consumerThread.Join();
        }
        static void Producer()
        {
            Random random = new Random();

            while (true)
            {
                int number = random.Next(1, 100);

                lock (lockObject)
                {
                    queue.Enqueue(number);
                    Console.WriteLine($"Виробник: Додано число {number} до черги");
                }

                Thread.Sleep(random.Next(500, 2000));
            }
        }
        static void Consumer()
        {
            while (true)
            {
                int number;

                lock (lockObject)
                {
                    if (queue.Count > 0)
                    {
                        number = queue.Dequeue();
                        Console.WriteLine($"Споживач: Взято число {number} з черги");
                    }
                    else
                    {
                        continue;
                    }
                }


                Console.WriteLine($"Споживач: Оброблено число {number}");

                Thread.Sleep(1000);
            }
        }
    }
}

