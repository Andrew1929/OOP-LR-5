using System;
using System.Threading;

namespace OOP_LR_5._2
{
    class Program
    {
        private static object lightLock = new object();
        private static Semaphore semaphore = new Semaphore(2, 2);
        static void Main(string[] args)
        {
            Thread[] trafficLightThreads = new Thread[4];

            for (int i = 0; i < 4; i++)
            {
                int lightIndex = i;
                trafficLightThreads[i] = new Thread(() => TrafficLight(lightIndex));
                trafficLightThreads[i].Start();
            }
            for (int i = 1; i <= 10; i++)
            {
                Thread carThread = new Thread(() => Car(i));
                carThread.Start();

                Thread.Sleep(1000);
            }
            foreach (Thread thread in trafficLightThreads)
            {
                thread.Join();
            }
        }
        static void TrafficLight(int lightIndex)
        {
            while (true)
            {
                lock (lightLock)
                {
                    Console.WriteLine($"Світлофор {lightIndex + 1}: Зелене");
                    Thread.Sleep(3000);
                    Console.WriteLine($"Світлофор {lightIndex + 1}: Жовте");
                    Thread.Sleep(1000);
                    Console.WriteLine($"Світлофор {lightIndex + 1}: Червоне");
                    Thread.Sleep(2000);
                }
            }
        }
        static void Car(int carNumber)
        {
            Console.WriteLine($"Автомобіль {carNumber}: Прибув до перехрестя");
            semaphore.WaitOne();
            Console.WriteLine($"Автомобіль {carNumber}: Проїжджає через перехрестя");
            Thread.Sleep(2000);
            semaphore.Release();
            Console.WriteLine($"Автомобіль {carNumber}: Покинув перехрестя");
        }
    }
}
