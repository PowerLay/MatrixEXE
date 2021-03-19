using System;
using System.Collections.Generic;

namespace MatrixLib
{
    public class Matrix
    {
        private readonly int _m;
        private readonly int _n;
        private readonly Random _rnd = new Random();
        private double _det;

        /// <summary>
        ///     Матрица n x m
        /// </summary>
        /// <param name="n">Количество строк матрицы</param>
        /// <param name="m">Количество столбцов матрицы</param>
        public Matrix(int n, int m)
        {
            _n = n;
            _m = m;
            Mtr = new double[n, m];
        }

        /// <summary>
        ///     Матрица n x n
        /// </summary>
        /// <param name="n">Количество строк квадратной матрицы</param>
        public Matrix(int n)
        {
            _n = n;
            _m = n;
            Mtr = new double[n, n];
        }

        /// <summary>
        ///     Матриц n x n с рандомным заполнением
        /// </summary>
        /// <param name="n">Количество строк квадратной матрицы</param>
        /// <param name="rndFill">
        ///     Заполнять значениями
        ///     (если false создаёт maxNum матрицу)
        /// </param>
        /// <param name="maxNum">
        ///     Максимальное значение ячейки
        ///     (от -maxNum до maxNum)
        /// </param>
        public Matrix(int n, bool rndFill, int maxNum)
        {
            _n = n;
            _m = n;
            Mtr = new double[n, n];

            if (rndFill)
                for (var i = 0; i < n; i++)
                for (var k = 0; k < _m; k++)
                    Mtr[i, k] = _rnd.Next(maxNum * 2) - maxNum;
            else
                for (var i = 0; i < n; i++)
                for (var k = 0; k < _m; k++)
                    Mtr[i, k] = maxNum;
        }

        /// <summary>
        ///     Матрица n x m с рандомным заполнением
        /// </summary>
        /// <param name="n">Количество строк матрицы</param>
        /// <param name="m">Количество столбцов матрицы</param>
        /// <param name="rndFill">
        ///     Заполнять значениями
        ///     (если false создаёт maxNum матрицу)
        /// </param>
        /// <param name="maxNum">
        ///     Максимальное значение ячейки
        ///     (от -maxNum до maxNum)
        /// </param>
        public Matrix(int n, int m, bool rndFill, int maxNum)
        {
            _n = n;
            _m = m;
            Mtr = new double[n, m];

            if (!rndFill) return;

            for (var i = 0; i < n; i++)
            for (var k = 0; k < m; k++)
                Mtr[i, k] = _rnd.Next(maxNum * 2) - maxNum;
        }

        public Matrix()
        {
        }

        private double[,] Mtr { get; set; }
        public bool IsSquare => Mtr.GetLength(0) == Mtr.GetLength(1);

        /// <summary>
        ///     Транспонированная матрица
        /// </summary>
        public Matrix Transpose
        {
            get
            {
                var row = Mtr.GetLength(0);
                var column = Mtr.GetLength(1);
                var mtrT = new Matrix(column, row)
                {
                    Mtr = Mtr.GetMtrT(),
                    _det = _det
                };
                return mtrT;
            }
        }

        /// <summary>
        ///     Определитель в строку
        /// </summary>
        private string DetToString
        {
            get
            {
                var str = "\n";
                var det = Det();

                if (Math.Abs(det) > 0.001 || det == 0)
                    str += det.ToString("#0.##");
                else
                    str += det.ToString("E3");
                return str;
            }
        }

        public double this[int i, int j] => Mtr[i, j];

        public static Matrix operator +(
            Matrix m1,
            Matrix m2)
        {
            if (m1 is null)
                throw new ArgumentNullException(nameof(m1));
            if (m1._n != m2._n && m1._m != m2._m)
                throw new ArgumentException("Wrong matrix size");

            var ans = new Matrix(m1._n, m1._m);
            for (var i = 0; i < ans._n; i++)
            for (var k = 0; k < ans._m; k++)
                ans.Mtr[i, k] = m1.Mtr[i, k] + m2.Mtr[i, k];
            return ans;
        }

        public static Matrix operator -(
            Matrix m1,
            Matrix m2)
        {
            if (m1._n != m2._n && m1._m != m2._m)
                throw new ArgumentException("Wrong matrix size");
            var ans = new Matrix(m1._n, m1._m);
            for (var i = 0; i < ans._n; i++)
            for (var k = 0; k < ans._m; k++)
                ans.Mtr[i, k] = m1.Mtr[i, k] + m2.Mtr[i, k];
            return ans;
        }

        public static Matrix operator *(
            double num,
            Matrix m1)
        {
            var ans = new Matrix(m1._n, m1._m);
            for (var i = 0; i < ans._n; i++)
            for (var k = 0; k < ans._m; k++)
                ans.Mtr[i, k] = m1.Mtr[i, k] * num;
            return ans;
        }

        public static Matrix operator *(
            Matrix m1,
            double num)
        {
            return num * m1;
        }

        public static Matrix operator *(
            Matrix m1,
            Matrix m2)
        {
            if (m1._m != m2._n)
                throw new ArgumentException("Wrong matrix size");

            var ans = new Matrix(m1._n, m2._m);
            for (var i = 0; i < ans._n; i++)
            for (var k = 0; k < ans._m; k++)
            {
                ans.Mtr[i, k] = 0;
                for (var j = 0; j < m1._m; j++) ans.Mtr[i, k] += m1.Mtr[i, j] * m2.Mtr[j, k];
            }

            return ans;
        }

        public static Matrix operator /(
            double num,
            Matrix m1)
        {
            var ans = new Matrix(m1._n, m1._m);
            for (var i = 0; i < ans._n; i++)
            for (var k = 0; k < ans._m; k++)
                ans.Mtr[i, k] = m1.Mtr[i, k] / num;
            return ans;
        }

        public static Matrix operator /(
            Matrix m1,
            double num)
        {
            return num / m1;
        }

        /// <summary>
        ///     Возвращает определитель матрицы
        /// </summary>
        /// <returns></returns>
        public double Det()
        {
            return _det != 0 ? _det : Mtr.Det();
        }

        /// <summary>
        ///     Возвращает обратную матрицу
        /// </summary>
        /// <returns></returns>
        public Matrix Revers()
        {
            if (_n != _m)
                throw new ArgumentException("Wrong matrix size");

            var ans = new Matrix(_n, _m);

            for (var i = 0; i < _n; i++)
            for (var k = 0; k < _m; k++)
                ans.Mtr[i, k] = AlgCofactors(i, k);
            ans = ans.Transpose;
            ans /= Det();
            return ans;
        }

        /// <summary>
        ///     Алгебраическое дополнение
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
        ///     Поиск минора матрицы
        /// </summary>
        /// <param name="row">Строка матрицы</param>
        /// <param name="column">Столбец матрицы</param>
        /// ///
        /// <returns>Возвращает минор элемента (row,column)</returns>
        public Matrix Minor(int row, int column)
        {
            var ans = new Matrix(row, column)
            {
                Mtr = Mtr.CutColumn(column).CutRow(row)
            };
            return ans;
        }

        /// <summary>
        ///     Преобразование матрицы в строку
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var str = "";
            var row = Mtr.GetLength(0);
            var column = Mtr.GetLength(1);
            for (var i = 0; i < row; i++)
            {
                for (var k = 0; k < column; k++) str += $"{Mtr[i, k]} \t";
                str += "\n";
            }

            return str;
        }

        /// <summary>
        ///     Преобразование матрицы в строку
        /// </summary>
        /// <param name="flagDet">Приписка определителя</param>
        /// <returns></returns>
        public string ToString(bool flagDet)
        {
            var str = ToString();
            if (flagDet && IsSquare)
                str += "Det: " + DetToString;

            return str;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is Matrix matrix &&
                   EqualityComparer<double[,]>.Default.Equals(Mtr, matrix.Mtr) &&
                   _n == matrix._n &&
                   _m == matrix._m;
        }
    }
}