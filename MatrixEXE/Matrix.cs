using System;
using System.Collections.Generic;
using System.Threading;

namespace Матрицы
{
    public class Matrix
    {
        readonly Random rnd = new Random();

        public double[,] Mtr { get; set; }
        public int n, m;
        private double det = 0;
        public bool IsSquare => Mtr.GetLength(0) == Mtr.GetLength(1);


        public static Matrix operator +(
            Matrix m1,
            Matrix m2)
        {
            if (m1 is null)
                throw new ArgumentNullException(nameof(m1));
            if (m1.n != m2.n && m1.m != m2.m)
                throw new ArgumentException("Wrong matrix size");

            Matrix ans = new Matrix(m1.n, m1.m);
            for (int i = 0; i < ans.n; i++)
            {
                for (int k = 0; k < ans.m; k++)
                {
                    ans.Mtr[i, k] = m1.Mtr[i, k] + m2.Mtr[i, k];
                }
            }
            return ans;
        }
        public static Matrix operator -(
            Matrix m1,
            Matrix m2)
        {
            if (m1.n != m2.n && m1.m != m2.m)
                throw new ArgumentException("Wrong matrix size");
            Matrix ans = new Matrix(m1.n, m1.m);
            for (int i = 0; i < ans.n; i++)
            {
                for (int k = 0; k < ans.m; k++)
                {
                    ans.Mtr[i, k] = m1.Mtr[i, k] + m2.Mtr[i, k];
                }
            }
            return ans;
        }
        public static Matrix operator *(
            double num,
            Matrix m1)
        {
            Matrix ans = new Matrix(m1.n, m1.m);
            for (int i = 0; i < ans.n; i++)
            {
                for (int k = 0; k < ans.m; k++)
                {
                    ans.Mtr[i, k] = m1.Mtr[i, k] * num;
                }
            }
            return ans;
        }
        public static Matrix operator *(
            Matrix m1,
            double num) => num * m1;

        public static Matrix operator *(
            Matrix m1,
            Matrix m2)
        {
            if (m1.m != m2.n)
                throw new ArgumentException("Wrong matrix size");

            Matrix ans = new Matrix(m1.n, m2.m);
            for (int i = 0; i < ans.n; i++)
            {
                for (int k = 0; k < ans.m; k++)
                {
                    ans.Mtr[i, k] = 0;
                    for (int j = 0; j < m1.m; j++)
                    {
                        ans.Mtr[i, k] += m1.Mtr[i, j] * m2.Mtr[j, k];
                    }
                }
            }
            return ans;
        }
        public static Matrix operator /(
            double num,
            Matrix m1)
        {
            Matrix ans = new Matrix(m1.n, m1.m);
            for (int i = 0; i < ans.n; i++)
            {
                for (int k = 0; k < ans.m; k++)
                {
                    ans.Mtr[i, k] = m1.Mtr[i, k] / num;
                }
            }
            return ans;
        }
        public static Matrix operator /(
            Matrix m1,
            double num) => num / m1;

        /// <summary>
        /// Мтрица n x m
        /// </summary>
        /// <param name="n">Колличество строк матрицы</param>
        /// <param name="m">Колличество столбецов матрицы</param>
        public Matrix(int n, int m)
        {
            this.n = n;
            this.m = m;
            Mtr = new double[n, m];
        }
        /// <summary>
        /// Мтрица n x n
        /// </summary>
        /// <param name="n">Колличество строк квадратной матрицы</param>
        public Matrix(int n)
        {
            this.n = n;
            this.m = n;
            Mtr = new double[n, n];
        }
        /// <summary>
        /// Мтрица n x n с рандомным заполнением
        /// </summary>
        /// <param name="n">Колличество строк квадратной матрицы</param>
        /// <param name="rndFill">Заполнять значениями
        /// (если false создаёт maxNum матрицу)
        /// </param>
        /// <param name="maxNum">Максимальное значение ячейки
        /// (от -maxNum до maxNum)</param>
        public Matrix(int n, bool rndFill, int maxNum)
        {
            this.n = n;
            this.m = n;
            Mtr = new double[n, n];

            if (rndFill)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int k = 0; k < m; k++)
                    {
                        Mtr[i, k] = rnd.Next(maxNum * 2) - maxNum;
                    }
                }
            }
            else
            {
                for (int i = 0; i < n; i++)
                {
                    for (int k = 0; k < m; k++)
                    {
                        Mtr[i, k] = maxNum;
                    }
                }
            }

        }
        /// <summary>
        /// Мтрица n x m с рандомным заполнением
        /// </summary>
        /// <param name="n">Колличество строк матрицы</param>
        /// <param name="m">Колличество столбецов матрицы</param>
        /// <param name="rndFill">Заполнять значениями
        /// (если false создаёт maxNum матрицу)
        /// </param>
        /// <param name="maxNum">Максимальное значение ячейки
        /// (от -maxNum до maxNum)</param>
        public Matrix(int n, int m, bool rndFill, int maxNum)
        {
            this.n = n;
            this.m = m;
            Mtr = new double[n, m];

            if (rndFill)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int k = 0; k < m; k++)
                    {
                        Mtr[i, k] = rnd.Next(maxNum * 2) - maxNum;
                    }
                }
            }
        }

        public Matrix()
        {
        }

        public void CnslOutput()
        {
            Mtr.CnslOutput();
            Console.WriteLine();
            //if (n == m)
            //    Console.WriteLine($"Det: {Det()}\n");
        }
        /// <summary>
        /// Возвращяет определитель матрицы
        /// </summary>
        /// <returns></returns>
        public double Det()
        {
            if (det != 0)
                return det;
            return Mtr.Det();
        }
        /// <summary>
        /// Возвращяет обратную матрицу
        /// </summary>
        /// <returns></returns>
        public Matrix Revers()
        {
            if (n != m)
                throw new ArgumentException("Wrong matrix size");

            var ans = new Matrix(n, m);

            for (int i = 0; i < n; i++)
            {
                for (int k = 0; k < m; k++)
                {
                    ans.Mtr[i, k] = AlgCofactors(i, k);
                }
            }
            ans = ans.Transpose;
            ans /= this.Det();
            return ans;
        }
        /// <summary>
        /// Алгебраическое дополнение
        /// </summary>
        /// <param name="n">Строка матрицы</param>
        /// <param name="m">Столбец матрицы</param>
        /// <returns>Возвращает алгебраическое дополнение элемента (n,m)</returns>
        public double AlgCofactors(int n, int m)
        {
            int factor;
            if ((n + m) % 2 == 0)
                factor = 1;
            else
                factor = -1;
            return factor * Mtr.Minor(n, m).Det();
        }

        /// <summary>
        /// Поиск минора матрицы
        /// </summary>
        /// <param name="row">Строка матрицы</param>
        /// <param name="column">Столбец матрицы</param>
        /// /// <returns>Возвращает минор элемента (row,column)</returns>
        public Matrix Minor(int row, int column)
        {
            var ans = new Matrix(row, column)
            {
                Mtr = Mtr.CutColumn(column).CutRow(row)
            };
            return ans;
        }
        /// <summary>
        /// Транспонированная матрица
        /// </summary>
        public Matrix Transpose
        {
            get
            {
                int row = Mtr.GetLength(0);
                int column = Mtr.GetLength(1);
                Matrix mtrT = new Matrix(column, row)
                {
                    Mtr = Mtr.GetMtrT(),
                    det = det
                };
                return mtrT;
            }
        }
        /// <summary>
        /// Преобразование матрицы в строку
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string str = "";
            int row = Mtr.GetLength(0);
            int column = Mtr.GetLength(1);
            for (int i = 0; i < row; i++)
            {
                for (int k = 0; k < column; k++)
                {
                    str += $"{Mtr[i, k]} \t";
                }
                str += "\n";
            }
            return str;
        }
        /// <summary>
        ///Преобразование матрицы в строку
        /// </summary>
        /// <param name="flagDet">Приписка определителя</param>
        /// <returns></returns>
        public string ToString(bool flagDet)
        {
            string str = ToString();
            if (flagDet && IsSquare)
                str += "Det: " + DetToString;

            return str;
        }

        public override bool Equals(object obj)
        {
            return obj is Matrix matrix &&
                   EqualityComparer<double[,]>.Default.Equals(Mtr, matrix.Mtr) &&
                   n == matrix.n &&
                   m == matrix.m;
        }

        /// <summary>
        /// Определитель в строку
        /// </summary>
        private string DetToString
        {
            get
            {
                var str = "\n";
                double det = Det();

                if (Math.Abs(det) > 0.001 || det == 0)
                    str += det.ToString("#0.##");
                else
                    str += det.ToString("E3");
                return str;
            }
        }
    }
}
