using System;
using System.Threading;

namespace OOP_LR_5._3
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = { 7, 2, 1, 6, 8, 5, 3, 4 };

            Console.WriteLine("Масив до сортування:");
            PrintArray(array);

            QuickSort(array, 0, array.Length - 1);

            Console.WriteLine("Масив після сортування:");
            PrintArray(array);
        }
        static void QuickSort(int[] array, int left, int right)
        {
            if (left < right)
            {
                int pivotIndex = Partition(array, left, right);

                Thread leftThread = new Thread(() => QuickSort(array, left, pivotIndex - 1));
                Thread rightThread = new Thread(() => QuickSort(array, pivotIndex + 1, right));

                leftThread.Start();
                rightThread.Start();

                leftThread.Join();
                rightThread.Join();
            }
        }
        static int Partition(int[] array, int left, int right)
        {
            int pivot = array[right];
            int i = left - 1;
            for (int j = left; j < right; j++)
            {
                if (array[j] < pivot)
                {
                    i++;
                    Swap(array, i, j);
                }
            }
            Swap(array, i + 1, right);
            return i + 1;
        }
        static void Swap(int[] array, int i, int j)
        {
            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
        static void PrintArray(int[] array)
        {
            foreach (int number in array)
            {
                Console.Write(number + " ");
            }
            Console.WriteLine();
        }
    }
}
