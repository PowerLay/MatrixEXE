using System;
using System.Collections;

namespace Матрицы
{
    class Program
    {
        static void Main()
        {
            int n, m, k;
            Console.WriteLine("Введи размер матрицы:");
            do
            {
                Console.Write("N = ");
            } while (!int.TryParse(Console.ReadLine(), out n));
            do
            {
                Console.Write("M = ");
            } while (!int.TryParse(Console.ReadLine(), out m));

            do
            {
                Console.Write("Введи диапазон(от -k до k) K=");
            } while (!int.TryParse(Console.ReadLine(), out k));
            Matrix a = new Matrix(n, m, true, k);
            Console.WriteLine("Матрица 1:");
            Console.WriteLine(a.ToString(true));

            Console.WriteLine("Введи размер матрицы:");
            do
            {
                Console.Write("N = ");
            } while (!int.TryParse(Console.ReadLine(), out n));
            do
            {
                Console.Write("M = ");
            } while (!int.TryParse(Console.ReadLine(), out m));

            do
            {
                Console.Write("Введи диапазон(от -k до k) K=");
            } while (!int.TryParse(Console.ReadLine(), out k));
            Matrix b = new Matrix(n, m, true, k);
            Console.WriteLine("Матрица 2:");
            Console.WriteLine(b.ToString(true));

            Console.WriteLine("Матрица 1*2:");
            Console.WriteLine((a * b).ToString(true));

        }
    }
}
