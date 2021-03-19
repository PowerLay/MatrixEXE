using System;

namespace MatrixLib
{
    internal static class Ext
    {
        public static double[,] GetMtrT(this double[,] a)
        {
            var row = a.GetLength(0);
            var column = a.GetLength(1);
            var mtrT = new double[column, row];
            for (var i = 0; i < column; i++)
            for (var k = 0; k < row; k++)
                mtrT[i, k] = a[k, i];
            return mtrT;
        }

        public static void ConsoleOutput(this double[,] mtr)
        {
            var row = mtr.GetLength(0);
            var column = mtr.GetLength(1);
            for (var i = 0; i < row; i++)
            {
                for (var k = 0; k < column; k++) Console.Write($"{mtr[i, k]} \t");
                Console.WriteLine();
            }
        }

        public static double Det(this double[,] mtr)
        {
            var row = mtr.GetLength(0);
            var column = mtr.GetLength(1);
            if (row != column)
                throw new InvalidOperationException("determinant can be calculated only for square matrix");

            if (row == 1)
                return mtr[0, 0];
            double ans = 0;
            for (var i = 0; i < column; i++)
            {
                int factor;
                if (i % 2 == 1)
                    factor = -1;
                else
                    factor = 1;
                ans += factor * mtr[0, i] * mtr.Minor(0, i).Det();
            }

            return ans;
        }

        public static double[,] Minor(
            this double[,] a,
            int row,
            int column)
        {
            return a.CutColumn(column).CutRow(row);
        }

        public static double[,] CutColumn(this double[,] mtr, int column)
        {
            var n = mtr.GetLength(0);
            var m = mtr.GetLength(1);
            if (column < 0 || column >= m)
                throw new ArgumentException("invalid column index");

            var result = new double[n, m - 1];
            for (var i = 0; i < n; i++)
            for (var k = 0; k < m - 1; k++)
                if (k >= column)
                    result[i, k] = mtr[i, k + 1];
                else
                    result[i, k] = mtr[i, k];
            return result;
        }

        public static double[,] CutRow(
            this double[,] mtr,
            int row)
        {
            var n = mtr.GetLength(0);
            var m = mtr.GetLength(1);
            if (row < 0 || row >= n)
                throw new ArgumentException("invalid column index");

            var result = new double[n - 1, m];
            for (var i = 0; i < n - 1; i++)
            for (var k = 0; k < m; k++)
                if (i >= row)
                    result[i, k] = mtr[i + 1, k];
                else
                    result[i, k] = mtr[i, k];
            return result;
        }
    }
}