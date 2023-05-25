using System;
using System.Threading;

namespace OOP_LR_5._4
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] matrixA = {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 9 }
        };
            int[,] matrixB = {
            { 9, 8, 7 },
            { 6, 5, 4 },
            { 3, 2, 1 }
        };
            int rowsA = matrixA.GetLength(0);
            int colsA = matrixA.GetLength(1);
            int rowsB = matrixB.GetLength(0);
            int colsB = matrixB.GetLength(1);
            if (colsA != rowsB)
            {
                Console.WriteLine("Неможливо виконати множення матриць. Кількість стовпців першої матриці має бути рівна кількості рядків другої матриці.");
                return;
            }
            int[,] resultMatrix = new int[rowsA, colsB];
            Semaphore semaphore = new Semaphore(2, 2);
            Thread[,] threads = new Thread[rowsA, colsB];
            for (int i = 0; i < rowsA; i++)
            {
                for (int j = 0; j < colsB; j++)
                {
                    semaphore.WaitOne();
                    int row = i;
                    int col = j;
                    threads[row, col] = new Thread(() => MultiplyAndAssign(matrixA, matrixB, resultMatrix, row, col, semaphore));
                    threads[row, col].Start();
                }
            }
            for (int i = 0; i < rowsA; i++)
            {
                for (int j = 0; j < colsB; j++)
                {
                    threads[i, j].Join();
                }
            }
            Console.WriteLine("Результуюча матриця:");
            PrintMatrix(resultMatrix);
        }
        static void MultiplyAndAssign(int[,] matrixA, int[,] matrixB, int[,] resultMatrix, int row, int col, Semaphore semaphore)
        {
            int value = 0;
            int colsA = matrixA.GetLength(1);
            for (int i = 0; i < colsA; i++)
            {
                value += matrixA[row, i] * matrixB[i, col];
            }
            lock (resultMatrix)
            {
                resultMatrix[row, col] = value;
            }
            semaphore.Release();
        }
        static void PrintMatrix(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }
    }
}

